using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Matrix
{

        class Matrix
        {
            // Скрытые поля
            private int n;
            private int[,] mass;

            // Создаем конструкторы матрицы
            public Matrix() { }

            public int N
            {
                get { return n; }
                set { if (value > 0) n = value; }
            }

            // Задаем аксессоры для работы с полями вне класса Matrix
            public Matrix(int n)
            {
                this.n = n;
                mass = new int[this.n, this.n];
            }

            public int this[int i, int j]
            {
                get
                {
                    return mass[i, j];
                }
                set
                {
                    mass[i, j] = value;
                }
            }

            // Ввод матрицы с клавиатуры
            public void WriteMat()
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Console.WriteLine("Введите элемент матрицы {0}:{1}", i + 1, j + 1);
                        mass[i, j] = Convert.ToInt32(Console.ReadLine());
                    }
                }
            }

            // Вывод матрицы с клавиатуры
            public void ReadMat()
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Console.Write(mass[i, j] + "\t");
                    }
                    Console.WriteLine();
                }
            }


            // Проверка матрицы А на единичность
            public void oneMat(Matrix a)
            {
                int count = 0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (a[i, j] == 1 && i == j)
                        {
                            count++;
                        }
                    }

                }
                if (count == a.N)
                {
                    Console.WriteLine("Единичная");
                }
                else Console.WriteLine("Не единичная");
            }


            // Умножение матрицы А на число
            public static Matrix umnch(Matrix a, int ch)
            {
                Matrix resMass = new Matrix(a.N);
                for (int i = 0; i < a.N; i++)
                {
                    for (int j = 0; j < a.N; j++)
                    {
                        resMass[i, j] = a[i, j] * ch;
                    }
                }
                return resMass;
            }

            // Умножение матрицы А на матрицу Б
            public static Matrix umn(Matrix a, Matrix b)
            {
                Matrix resMass = new Matrix(a.N);
                for (int i = 0; i < a.N; i++)
                    for (int j = 0; j < b.N; j++)
                        for (int k = 0; k < b.N; k++)
                            resMass[i, j] += a[i, k] * b[k, j];

                return resMass;
            }



            // перегрузка оператора умножения
            public static Matrix operator *(Matrix a, Matrix b)
            {
                return Matrix.umn(a, b);
            }

            public static Matrix operator *(Matrix a, int b)
            {
                return Matrix.umnch(a, b);
            }


            // Метод вычитания матрицы Б из матрицы А
            public static Matrix razn(Matrix a, Matrix b)
            {
                Matrix resMass = new Matrix(a.N);
                for (int i = 0; i < a.N; i++)
                {
                    for (int j = 0; j < b.N; j++)
                    {
                        resMass[i, j] = a[i, j] - b[i, j];
                    }
                }
                return resMass;
            }

            // Перегрузка оператора вычитания
            public static Matrix operator -(Matrix a, Matrix b)
            {
                return Matrix.razn(a, b);
            }
            public static Matrix Sum(Matrix a, Matrix b)
            {
                Matrix resMass = new Matrix(a.N);
                for (int i = 0; i < a.N; i++)
                {
                    for (int j = 0; j < b.N; j++)
                    {
                        resMass[i, j] = a[i, j] + b[i, j];
                    }
                }
                return resMass;
            }
            // Перегрузка сложения
            public static Matrix operator +(Matrix a, Matrix b)
            {
                return Matrix.Sum(a, b);
            }
            // Деструктор Matrix
            ~Matrix()
            {
                Console.WriteLine("Очистка");
            }

        }
        class MainProgram
        {

            static void Main(string[] args)
            {
                Console.WriteLine("Введите размерность матрицы: ");
                int nn = Convert.ToInt32(Console.ReadLine());
                // Инициализация
                Matrix mass1 = new Matrix(nn);
                Matrix mass2 = new Matrix(nn);
                Matrix mass3 = new Matrix(nn);
                Matrix mass4 = new Matrix(nn);
                Matrix mass5 = new Matrix(nn);
                Matrix mass6 = new Matrix(nn);
                Matrix mass7 = new Matrix(nn);
                Matrix mass8 = new Matrix(nn);
                Console.WriteLine("ввод Матрица А: ");
                mass1.WriteMat();
                Console.WriteLine("Ввод Матрица B: ");
                mass2.WriteMat();

                Console.WriteLine("Матрица А: ");
                mass1.ReadMat();
                Console.WriteLine();
                Console.WriteLine("Матрица В: ");
                Console.WriteLine();
                mass2.ReadMat();

                Console.WriteLine("Сложение матриц А и Б: ");
                mass4 = (mass1 + mass2);
                mass4.ReadMat();

                Console.WriteLine("Вычитание матриц А и Б: ");
                mass6 = (mass1 - mass2);
                mass6.ReadMat();

                Console.WriteLine("Умножение матриц А и Б: ");
                mass8 = (mass1 * mass2);
                mass8.ReadMat();

                Console.WriteLine("Умножение матрицы А на число 2: ");
                mass5 = (mass1 * 2);
                mass5.ReadMat();

                Console.WriteLine("Матрица D по формуле  D=3AB+(A-B)A: ");
                mass7 = ((mass1 * 3) * mass2 + (mass1 - mass2) * mass1);
                mass7.ReadMat();


                Console.ReadKey();
            }
        }
    }
