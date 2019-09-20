using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBF
{
   public class Test
   {

       public string Conection =
           "Data Source=i7751-app020;Initial Catalog=imns51;Integrated Security=True;MultipleActiveResultSets=True";

       public string Selectxml = @"Select RTRIM(face.TAB_NUM) as TAB_NUM,RTRIM(face.NEW_POST) as NEW_POST,RTRIM(sub.NAME) as NAME From dbo.STAFF staf
                      Join (Select I1.TAB_NUM,FM,IM,OT,NEW_SUBDIV,NEW_POST,STAFF_LINK FROM ITEM_MOVE I1
                             Join (Select LINK_EMPL, MAX(LINK) as LINK From ITEM_MOVE
                                   GROUP BY LINK_EMPL) I2 on I1.LINK = I2.LINK 
                             Join EMPLOYERS_TBL emp on emp.LINK=I2.LINK_EMPL and emp.DATE_OUT is not null) face on face.STAFF_LINK = staf.LINK
                     LEFT Join dbo.SUBDIV sub on sub.LINK_UP = staf.SUBDIV_LINK  For Xml Auto";
   


    public string Actuals = @"Select RTRIM(face.TAB_NUM) as TAB_NUM,RTRIM(face.NEW_POST) as NEW_POST,RTRIM(sub.NAME) as NAME,RTRIM(face.IM) as IM,RTRIM(face.OT) as OT,RTRIM(face.FM) as FM From dbo.STAFF staf
                      Join (Select I1.TAB_NUM,FM,IM,OT,NEW_SUBDIV,NEW_POST,STAFF_LINK FROM ITEM_MOVE I1
                             Join (Select LINK_EMPL, MAX(LINK) as LINK From ITEM_MOVE
                                   GROUP BY LINK_EMPL) I2 on I1.LINK = I2.LINK 
                             Join EMPLOYERS_TBL emp on emp.LINK=I2.LINK_EMPL and emp.DATE_OUT is null) face on face.STAFF_LINK = staf.LINK
                     LEFT Join dbo.SUBDIV sub on sub.LINK_UP = staf.SUBDIV_LINK  For Xml Auto";
}
}
