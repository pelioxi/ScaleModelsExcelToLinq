using System.Linq;

namespace ScaleModelsExcelToLinq.Models
{
    public class UserInfoModel
    {
        public UserInformation GetUserInformation(string guId)
        {
            ScaleModelsExcelToLinqEntities db = new ScaleModelsExcelToLinqEntities();
            UserInformation info = (from x in db.UserInformations
                                    where x.GUID == guId
                                    select x).FirstOrDefault();

            return info;
        }

        public void InsertUserInformation(UserInformation info)
        {
            ScaleModelsExcelToLinqEntities db = new ScaleModelsExcelToLinqEntities();
            db.UserInformations.Add(info);
            db.SaveChanges();
        }
    }
}