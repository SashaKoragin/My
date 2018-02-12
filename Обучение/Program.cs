
// CommonComponents.Hosting.Program
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

internal static class Program
{
    private static class ParameterNames
    {
        public const string Debug = "debug";

        public const string Config = "config";
    }

    private const char ParameterValueSeparatorChar = '=';

    private static readonly char[] ParameterSigns = new char[2]
    {
        '/',
        '-'
    };

    private static Type HostingExType;

    private static Type HostingStarterType;

    private static object HostingStarter;

    private static int MainThreadId;

    [STAThread]
    [LoaderOptimization(LoaderOptimization.MultiDomain)]
    private static void Main(string[] args)
    {
        try
        {
            IDictionary<string, string> dictionary = default(IDictionary<string, string>);
            if (Program.TryParseArguments(args, out dictionary))
            {
                Program.MainThreadId = Thread.CurrentThread.ManagedThreadId;
                Thread.CurrentThread.Name = "MainThread";
                bool flag = default(bool);
                if (Program.TryGetOptionalParameterValueOrDefault(dictionary, "debug", false, out flag))
                {
                    if (flag)
                    {
                        Debugger.Launch();
                    }
                    string configurationFile = default(string);
                    if (!dictionary.TryGetValue("config", out configurationFile))
                    {
                        configurationFile = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                    }
                    AppDomain.CurrentDomain.UnhandledException += Program.AppDomainUnhandledException;
                    using (SplashManager splashManager = new SplashManager())
                    {
                        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    //    AssemblyCache assemblyCache = AssemblyCache.LoadFromFile(baseDirectory);
                     //   assemblyCache.AttachToDomain(AppDomain.CurrentDomain);
                        AppDomain.CurrentDomain.SetData("splashManager", splashManager);
                        Program.HostingExType = Type.GetType("CommonComponents.Hosting.Common.HostingException, CommonComponents.Hosting.Common", true, true);
                        Program.HostingStarterType = Type.GetType("CommonComponents.Hosting.Common.HostingStarter, CommonComponents.Hosting.Common", true, true);
                        Program.HostingStarter = Activator.CreateInstance(Program.HostingStarterType);
                        splashManager.ShowSplash(Program.RequestHostingShutDown);
                        Program.HostingStarterType.InvokeMember("Initialize", BindingFlags.InvokeMethod, Type.DefaultBinder, Program.HostingStarter, new object[2]
                        {
                            configurationFile,
                            new Action(Program.SignalToShutDown)
                        });
                        Program.HostingStarterType.InvokeMember("Start", BindingFlags.InvokeMethod, Type.DefaultBinder, Program.HostingStarter, null);
                        Program.HostingStarterType.InvokeMember("Stop", BindingFlags.InvokeMethod, Type.DefaultBinder, Program.HostingStarter, null);
                    }
                }
            }
        }
        catch (TargetInvocationException ex)
        {
            Program.HandleException(ex.InnerException);
        }
        catch (Exception ex2)
        {
            Program.HandleException(ex2);
        }
    }

    private static void RequestHostingShutDown()
    {
        Program.HostingStarterType.InvokeMember("RequestHostingShutDown", BindingFlags.InvokeMethod, Type.DefaultBinder, Program.HostingStarter, null);
    }

    private static void AppDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        Program.HandleException(e.ExceptionObject as Exception);
    }

    private static void HandleException(Exception ex)
    {
        if (!object.ReferenceEquals(ex, null))
        {
            Program.LogException(ex);
            string message = ex.ToString();
            Program.ShowErrorMessage(message);
        }
    }

    private static void ShowErrorMessage(string message)
    {
        if (Thread.CurrentThread.ManagedThreadId == Program.MainThreadId)
        {
            Form form = new Form();
            form.WindowState = FormWindowState.Minimized;
            form.ShowInTaskbar = false;
            using (Form form2 = form)
            {
                form2.Show();
                MessageBox.Show(form2, message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }

    private static void SignalToShutDown()
    {
    }

    private static void LogException(Exception ex)
    {
        if (!object.ReferenceEquals(Program.HostingExType, null) && ex.GetType() == Program.HostingExType)
        {
            return;
        }
        EventLog.WriteEvent("Application Error", new EventInstance(1000L, 100, EventLogEntryType.Error), Environment.CommandLine, "Unknown version", DateTime.Now, typeof(Program).Assembly.GetName().Name, typeof(Program).Assembly.GetName().Version, DateTime.MinValue, ex.ToString(), "", "", DateTime.MinValue, "", "", 0);
    }

    private static bool TryParseArguments(string[] args, out IDictionary<string, string> parameters)
    {
        parameters = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        int num = args.Length;
        for (int i = 0; i < num; i++)
        {
            string text = args[i];
            char value = text[0];
            if (!Program.ParameterSigns.Contains(value))
            {
                Program.ShowErrorMessage("Обнаружен аргумент запуска [" + text + "], не являющийся параметром. " + "Аргументы запуска, не являющиеся параметрами, не поддерживаются. " + "Выполните запуск приложения с параметром /? или /help для получения справки по поддерживамым параметрам.");
                parameters = null;
                return false;
            }
            int num2 = text.IndexOf('=');
            string text2;
            string value2;
            if (num2 > -1)
            {
                text2 = text.Substring(1, num2 - 1);
                value2 = text.Substring(num2 + 1);
            }
            else if (num2 == text.Length - 1)
            {
                text2 = text.Substring(1, num2 - 1);
                value2 = string.Empty;
            }
            else
            {
                text2 = text.Substring(1);
                value2 = string.Empty;
            }
            if (parameters.ContainsKey(text2))
            {
                Program.ShowErrorMessage("Обнаружено дублирование параметра запуска с именем [" + text2 + "]. " + "Дублирующиеся параметры запуска не поддерживаются.");
                parameters = null;
                return false;
            }
            parameters.Add(text2, value2);
        }
        return true;
    }

    private static bool TryGetOptionalParameterValueOrDefault(IDictionary<string, string> parameters, string parameterName, bool notExistsValue, out bool parameterValue)
    {
        string text = default(string);
        if (!parameters.TryGetValue(parameterName, out text))
        {
            parameterValue = notExistsValue;
            return true;
        }
        if (string.IsNullOrEmpty(text))
        {
            parameterValue = true;
            return true;
        }
        if (!bool.TryParse(text, out parameterValue))
        {
            Program.ShowErrorMessage("Обнаружено некорректное значение параметра запуска с именем [" + parameterName + "]: [" + text + "]." + "Поддерживаемые значения: пустая строка, [" + bool.TrueString + "], [" + bool.FalseString + "].");
            return false;
        }
        return true;
    }
}
