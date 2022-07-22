using Microsoft.AspNet.Identity;
using ScaleModelsExcelToLinq.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;

namespace ScaleModelsExcelToLinq
{
    public partial class Index : System.Web.UI.Page
    {
        private string FilePath = string.Empty;
        private static readonly string fileName = "modelos.xls";
        private static int NumRecords = 0;
        private static bool sw_1 = false;
        private static bool sw_2 = false;
        private static bool sw_3 = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            FilePath = Server.MapPath("~/db/" + fileName);

            if (!IsPostBack)
            {
                DDLFirstLoad();
            }
            else
            {
                string clientID = Context.User.Identity.GetUserId();

                if (clientID != null)
                {
                    DDL_Setup();

                    List<ScaleModels> theProducts = GetProducts();

                    if (theProducts.Count() > 50)
                    {
                        LblResult.Text = "Too many rows to show. Please use drop down lists for shorten search criteria.";
                    }
                    else
                    {
                        NumRecords = theProducts.Count();

                        LblResult.Text = "showing " + NumRecords.ToString("N0") + " models actually";
                        ShowProducts(theProducts);
                    }
                }
                else
                {
                    LblResult.Text = "Please log in (or, first, register) in order to see our amazing 'all-timer' vehicles collection...";
                }
            }
        }

        protected void Clear_Selection_Click(object sender, EventArgs e)
        {
            DDLFirstLoad();
        }

        private void ShowProducts(List<ScaleModels> theProducts)
        {

            foreach (ScaleModels item in theProducts)
            {
                Panel productPanel = new Panel();
                ImageButton imageButton = new ImageButton();
                Label lblName = new Label();
                Label lblColMaker = new Label();

                // Set childcontrol's properties
                if (File.Exists(Server.MapPath("~/Images/" + item.Scale18 + "/01.jpg")))
                {
                    imageButton.ImageUrl = "~/Images/" + item.Scale18 + "/01.jpg";
                }
                else
                {
                    imageButton.ImageUrl = "~/Images/nophoto.jpg";
                };

                imageButton.CssClass = "productImage";

                if (!String.IsNullOrEmpty(item.Scale18))
                {
                    imageButton.PostBackUrl = "~/Pages/Product.aspx?id=" + item.Scale18;
                }
                else
                {
                    imageButton.PostBackUrl = "";
                }
                imageButton.AlternateText = item.Scale18;

                lblName.Text = item.Brand + " " + item.Model + " (" + item.CarYear + ")";
                lblName.CssClass = "productName";
                lblColMaker.Text = item.Collection + " - " + item.Maker;
                lblColMaker.CssClass = "productName";

                // Add Child Controls to Parent Panel
                productPanel.Controls.Add(imageButton);
                productPanel.Controls.Add(new Literal { Text = "<br />" });
                productPanel.Controls.Add(lblName);
                productPanel.Controls.Add(new Literal { Text = "<br />" });
                productPanel.Controls.Add(lblColMaker);

                // Add dynamic panel to static parent panle
                PnlProducts.Controls.Add(productPanel);
            }

        }


        private List<ScaleModels> GetProducts()
        {
            ConnexionExcel db = new ConnexionExcel(FilePath);
            db.UrlConnexion.UsePersistentConnection = false;

            try
            {

                if (sw_1)
                {
                    if (sw_2)
                    {
                        if (sw_3)
                        {
                            // Case 7 - 1 1 1 : Just show Products
                            
                            var ProductsLst = (from x in db.UrlConnexion.Worksheet<ScaleModels>()
                                               where x.Collection == DDLCollection.SelectedValue &&
                                                            x.Brand == DDLBrand.SelectedValue &&
                                                            x.Maker == DDLMaker.SelectedValue
                                               orderby x.CarYear, x.Brand, x.Model
                                               select x).ToList();

                            return ProductsLst;
                        }
                        else
                        {
                            // Case 4 - 1 1 0
                            
                            var ProductsLst = (from x in db.UrlConnexion.Worksheet<ScaleModels>()
                                               where x.Collection == DDLCollection.SelectedValue &&
                                                            x.Brand == DDLBrand.SelectedValue
                                               orderby x.CarYear, x.Brand, x.Model
                                               select x).ToList();

                            return ProductsLst;
                        }
                    }
                    else
                    {
                        if (sw_3)
                        {
                            // Case 5 - 1 0 1

                            var ProductsLst = (from x in db.UrlConnexion.Worksheet<ScaleModels>()
                                               where x.Collection == DDLCollection.SelectedValue &&
                                                            x.Maker == DDLMaker.SelectedValue
                                               orderby x.CarYear, x.Brand, x.Model
                                               select x).ToList();

                            return ProductsLst;
                        }
                        else
                        {
                            // Case 1 - 1 0 0

                            var ProductsLst = (from x in db.UrlConnexion.Worksheet<ScaleModels>()
                                               where x.Collection == DDLCollection.SelectedValue
                                               orderby x.CarYear, x.Brand, x.Model
                                               select x).ToList();

                            return ProductsLst;
                        }
                    }
                }
                else
                {
                    if (sw_2)
                    {
                        if (sw_3)
                        {
                            // Case 6 - 0 1 1

                            var ProductsLst = (from x in db.UrlConnexion.Worksheet<ScaleModels>()
                                               where x.Brand == DDLBrand.SelectedValue &&
                                                     x.Maker == DDLMaker.SelectedValue
                                               orderby x.CarYear, x.Brand, x.Model
                                               select x).ToList();

                            return ProductsLst;
                        }
                        else
                        {
                            // Case 2 - 0 1 0

                            var ProductsLst = (from x in db.UrlConnexion.Worksheet<ScaleModels>()
                                               where x.Brand == DDLBrand.SelectedValue
                                               orderby x.CarYear, x.Brand, x.Model
                                               select x).ToList();

                            return ProductsLst;
                        }
                    }
                    else
                    {
                        if (sw_3)
                        {
                            // Case 3 - 0 0 1

                            var ProductsLst = (from x in db.UrlConnexion.Worksheet<ScaleModels>()
                                               where x.Maker == DDLMaker.SelectedValue
                                               orderby x.CarYear, x.Brand, x.Model
                                               select x).ToList();

                            return ProductsLst;
                        }
                        else
                        {
                            // Case 0 - 0 0 0

                            var ProductsLst = (from x in db.UrlConnexion.Worksheet<ScaleModels>()
                                               orderby x.CarYear, x.Brand, x.Model
                                               select x).ToList();

                            return ProductsLst;
                        }
                    }
                }
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

        private void DDL_Setup()
        {
            sw_1 = DDLCollection.SelectedIndex != 0;
            sw_2 = DDLBrand.SelectedIndex != 0;
            sw_3 = DDLMaker.SelectedIndex != 0;

            ConnexionExcel db = new ConnexionExcel(FilePath);
            db.UrlConnexion.UsePersistentConnection = false;

            try
            {
                if (sw_1)
                {
                    if (sw_2)
                    {
                        if (sw_3)
                        {
                            // Case 7 - 1 1 1 : Just show Products

                        }
                        else
                        {
                            // Case 4 - 1 1 0
                            var LstDDL = (from x in db.UrlConnexion.Worksheet<ScaleModels>()
                                          where x.Collection == DDLCollection.SelectedValue &&
                                          x.Brand == DDLBrand.SelectedValue
                                          orderby x.Maker
                                          select x.Maker).Distinct();

                            DDLMaker.DataSource = LstDDL;
                            DDLMaker.DataBind();
                            DDLMaker.Items.Insert(0, new ListItem("--Select Maker--", "0"));
                        }
                    }
                    else
                    {
                        if (sw_3)
                        {
                            // Case 5 - 1 0 1
                            var LstDDL = (from x in db.UrlConnexion.Worksheet<ScaleModels>()
                                          where x.Collection == DDLCollection.SelectedValue &&
                                          x.Maker == DDLMaker.SelectedValue
                                          orderby x.Brand
                                          select x.Brand).Distinct();

                            DDLBrand.DataSource = LstDDL;
                            DDLBrand.DataBind();
                            DDLBrand.Items.Insert(0, new ListItem("--Select Brand--", "0"));
                        }
                        else
                        {
                            // Case 1 - 1 0 0
                            var LstDDL = (from x in db.UrlConnexion.Worksheet<ScaleModels>()
                                          where x.Collection == DDLCollection.SelectedValue
                                          orderby x.Brand
                                          select x.Brand).Distinct();

                            DDLBrand.DataSource = LstDDL;
                            DDLBrand.DataBind();
                            DDLBrand.Items.Insert(0, new ListItem("--Select Brand--", "0"));

                            LstDDL = (from x in db.UrlConnexion.Worksheet<ScaleModels>()
                                      where x.Collection == DDLCollection.SelectedValue
                                      orderby x.Maker
                                      select x.Maker).Distinct();

                            DDLMaker.DataSource = LstDDL;
                            DDLMaker.DataBind();
                            DDLMaker.Items.Insert(0, new ListItem("--Select Maker--", "0"));

                        }
                    }
                }
                else
                {
                    if (sw_2)
                    {
                        if (sw_3)
                        {
                            // Case 6 - 0 1 1
                            var LstDDL = (from x in db.UrlConnexion.Worksheet<ScaleModels>()
                                          where x.Brand == DDLBrand.SelectedValue &&
                                          x.Maker == DDLMaker.SelectedValue
                                          orderby x.Collection
                                          select x.Collection).Distinct();

                            DDLCollection.DataSource = LstDDL;
                            DDLCollection.DataBind();
                            DDLCollection.Items.Insert(0, new ListItem("--Select Collection--", "0"));

                        }
                        else
                        {
                            // Case 2 - 0 1 0
                            var LstDDL = (from x in db.UrlConnexion.Worksheet<ScaleModels>()
                                          where x.Brand == DDLBrand.SelectedValue
                                          orderby x.Collection
                                          select x.Collection).Distinct();

                            DDLCollection.DataSource = LstDDL;
                            DDLCollection.DataBind();
                            DDLCollection.Items.Insert(0, new ListItem("--Select Collection--", "0"));

                            LstDDL = (from x in db.UrlConnexion.Worksheet<ScaleModels>()
                                          where x.Brand == DDLBrand.SelectedValue
                                          orderby x.Maker
                                          select x.Maker).Distinct();

                            DDLMaker.DataSource = LstDDL;
                            DDLMaker.DataBind();
                            DDLMaker.Items.Insert(0, new ListItem("--Select Maker--", "0"));
                        }
                    }
                    else
                    {
                        if (sw_3)
                        {
                            // Case 3 - 0 0 1
                            var LstDDL = (from x in db.UrlConnexion.Worksheet<ScaleModels>()
                                          where x.Maker == DDLMaker.SelectedValue
                                          orderby x.Collection
                                          select x.Collection).Distinct();

                            DDLCollection.DataSource = LstDDL;
                            DDLCollection.DataBind();
                            DDLCollection.Items.Insert(0, new ListItem("--Select Collection--", "0"));

                            LstDDL = (from x in db.UrlConnexion.Worksheet<ScaleModels>()
                                      where x.Maker == DDLMaker.SelectedValue
                                      orderby x.Brand
                                      select x.Brand).Distinct();

                            DDLBrand.DataSource = LstDDL;
                            DDLBrand.DataBind();
                            DDLBrand.Items.Insert(0, new ListItem("--Select Brand--", "0"));
                        }
                        else
                        {
                            // Case 0 - 0 0 0

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LitStatus.Text = ex.ToString();
            }
            finally
            {
                db.UrlConnexion.Dispose();
            }
        }

        private void DDLFirstLoad()
        {
            ConnexionExcel ConxObject = new ConnexionExcel(FilePath);
            ConxObject.UrlConnexion.UsePersistentConnection = false;

            try
            {
                var LstObject = (from x in ConxObject.UrlConnexion.Worksheet<ScaleModels>()
                                 orderby x.Collection
                                 select x.Collection).Distinct();

                DDLCollection.DataSource = LstObject;
                DDLCollection.DataBind();
                DDLCollection.Items.Insert(0, new ListItem("--Select Collection--", "0"));


                LstObject = (from x in ConxObject.UrlConnexion.Worksheet<ScaleModels>()
                             orderby x.Brand
                             select x.Brand).Distinct();

                DDLBrand.DataSource = LstObject;
                DDLBrand.DataBind();
                DDLBrand.Items.Insert(0, new ListItem("--Select Brand--", "0"));

                LstObject = (from x in ConxObject.UrlConnexion.Worksheet<ScaleModels>()
                             orderby x.Maker
                             select x.Maker).Distinct();

                DDLMaker.DataSource = LstObject;
                DDLMaker.DataBind();
                DDLMaker.Items.Insert(0, new ListItem("--Select Maker--", "0"));

            }
            catch (Exception ex)
            {
                LitStatus.Text = ex.ToString();
            }
            finally
            {
                ConxObject.UrlConnexion.Dispose();
            }
        }
    }
}