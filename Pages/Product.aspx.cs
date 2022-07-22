using Microsoft.AspNet.Identity;
using ScaleModelsExcelToLinq.Models;
using System;
using System.IO;
using System.Linq;

namespace ScaleModelsExcelToLinq.Pages
{
    public partial class Product : System.Web.UI.Page
    {
        private static string FilePath = string.Empty;
        private static readonly string fileName = "modelos.xls";
        private static string id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            string clientID = Context.User.Identity.GetUserId();

            if (clientID != null)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["id"]))
                {
                    id = Request.QueryString["id"];

                    if (!IsPostBack)
                    {
                        FilePath = Server.MapPath("~/db/" + fileName);
                    }

                    ScaleModels theProduct = GetProductById();

                    if (theProduct != null)
                    {
                        FillPage(theProduct);
                    }
                }
            }
            else
            {
                LitStatus.Text = "Please log in (or, first, register) in order to see our amazing 'all-timer' vehicles collection...";
            }
        }

        private ScaleModels GetProductById()
        {
            ConnexionExcel db = new ConnexionExcel(FilePath);
            db.UrlConnexion.UsePersistentConnection = false;

            try
            {
                ScaleModels scaleModels = (from x in db.UrlConnexion.Worksheet<ScaleModels>()
                                           where x.Scale18 == id
                                           select x).FirstOrDefault();
                return scaleModels;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                db.UrlConnexion.Dispose();
            }
        }

        private void FillPage(ScaleModels scaleModels)
        {
            Label1.Text = scaleModels.Collection;
            Label2.Text = scaleModels.Brand;
            Label3.Text = scaleModels.Model;
            Label4.Text = scaleModels.CarYear;
            Label5.Text = scaleModels.Maker;
            Label6.Text = scaleModels.Scale;
            Label7.Text = scaleModels.PartNumber;
            Label8.Text = scaleModels.CarNumber;
            Label9.Text = scaleModels.ColourSponsor;
            Label10.Text = scaleModels.Driver;
            TextBox1.Text = scaleModels.Details;
            TextBox1.Enabled = false;
            Label11.Text = scaleModels.ModelDate;
            Label12.Text = scaleModels.Serial;
            Label13.Text = scaleModels.Ledition;
            TextBox2.Text = scaleModels.Comments;
            TextBox2.Enabled = false;

            if (!String.IsNullOrEmpty(id))
            {
                if (Directory.Exists(Server.MapPath("~/Images/" + id + "/"))) 
                {
                    GetImages();
                }
                else
                {
                    Image1.ImageUrl = "~/Images/nophoto.jpg";
                    Image1.AlternateText = Image1.ImageUrl;
                    Image2.ImageUrl = "~/Images/nophoto.jpg";
                    Image2.AlternateText = Image2.ImageUrl;
                    Image3.ImageUrl = "~/Images/nophoto.jpg";
                    Image3.AlternateText = Image3.ImageUrl;
                    Image4.ImageUrl = "~/Images/nophoto.jpg";
                    Image4.AlternateText = Image4.ImageUrl;
                }
            }
            else
            {
                Image1.ImageUrl = "~/Images/nophoto.jpg";
                Image1.AlternateText = Image1.ImageUrl;
                Image2.ImageUrl = "~/Images/nophoto.jpg";
                Image2.AlternateText = Image2.ImageUrl;
                Image3.ImageUrl = "~/Images/nophoto.jpg";
                Image3.AlternateText = Image3.ImageUrl;
                Image4.ImageUrl = "~/Images/nophoto.jpg";
                Image4.AlternateText = Image4.ImageUrl;
            }
        }

        private void GetImages()
        {
            string[] images = Directory.GetFiles(Server.MapPath("~/Images/" + id + "/"));

            int i = 0;

            foreach(string image in images)
            {
                string imageName = image.Substring(image.LastIndexOf(@"\", StringComparison.Ordinal) + 1);
                string ext = Path.GetExtension(imageName).ToLower();
                if (ext == ".jpg" || ext == ".jpeg")
                {
                    switch (i)
                    {
                        case 0:
                            Image1.ImageUrl = "~/Images/" + id + "/" + imageName;
                            Image1.AlternateText = imageName;
                            break;
                        case 1:
                            Image2.ImageUrl = "~/Images/" + id + "/" + imageName;
                            Image1.AlternateText = imageName;
                            break;
                        case 2:
                            Image3.ImageUrl = "~/Images/" + id + "/" + imageName;
                            Image1.AlternateText = imageName;
                            break;
                        case 3:
                            Image4.ImageUrl = "~/Images/" + id + "/" + imageName;
                            Image1.AlternateText = imageName;
                            break;
                        default:
                            break;
                    }

                    i += 1;
                }
            }
        }
    }
}