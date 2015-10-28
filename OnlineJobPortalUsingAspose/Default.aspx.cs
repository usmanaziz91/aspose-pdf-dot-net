using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Aspose.Pdf;
using Aspose.Pdf.Text;

using Aspose.Pdf.Text.TextOptions;
using Aspose.Pdf.Generator;
using System.Data;
using System.IO;


namespace OnlineJobPortalUsingAspose
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                msg.Text = "";
                BindData();
            }
        }

        private void BindData()
        {
            try
            {
                List<int> list = new List<int>();
                for (int i = 1; i < 5; i++)
                {
                    list.Add(i);
                } 
                gvEducationalDetails.DataSource = list;
                gvEducationalDetails.DataBind();

                gvExperience.DataSource = list;
                gvExperience.DataBind();

                //Lisence verification code...
                //-----------------------------------------
                //Aspose.Pdf.License pdflicense = new Aspose.Pdf.License();
                //pdflicense.SetLicense(Server.MapPath("Aspose.Pdf.lic"));
                //pdflicense.Embedded = true;
                //------------------------------------------
                 
            }
            catch { }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                 
                //Creating new document
                Document doc = new Document();
                //add a new page in the document...
                Aspose.Pdf.Page page = doc.Pages.Add(); 
                
                //add header and footer in the document
                AddHeaderFooter(doc, "Online Application Form - Dated: " + DateTime.Now.Date.ToString("MM-dd-yyyy"), "");
                 

                //crearing new table
                Aspose.Pdf.Table table = new Aspose.Pdf.Table();
                page.Paragraphs.Add(table); 
                table.Alignment = Aspose.Pdf.HorizontalAlignment.Center;
                table.DefaultColumnWidth = "500"; 
                //create a new row in the table...
                Aspose.Pdf.Row row = new Aspose.Pdf.Row();
                table.Rows.Add(row);
                //create a new cell in the row...
                Aspose.Pdf.Cell cell = new Aspose.Pdf.Cell();
                row.Cells.Add(cell);

                cell.Alignment = Aspose.Pdf.HorizontalAlignment.Center;


                //create main heading of the page
                TextFragment txtFragmanet = new TextFragment("Online Application Form");
                
                txtFragmanet.TextState.FontSize = 15;
                txtFragmanet.TextState.FontStyle = FontStyles.Bold;
                cell.Paragraphs.Add(txtFragmanet);

                //============================ Section of personal information Starts======================
                // Add a new heading ...
                AddHeading(page, "Applied for Position: " + txtPosition.Text, 8, false);
                
                // Add a new heading ...
                AddHeading(page, "Personal Information:", 10, true);

                // create table for personal information 
                Aspose.Pdf.Table tblPersonalInfo = new Aspose.Pdf.Table();
                page.Paragraphs.Add(tblPersonalInfo);
                tblPersonalInfo.DefaultCellTextState.FontSize = 6;
                //set columns width...
                tblPersonalInfo.ColumnWidths = "100 400";

                //adding personal details ...
                AddRow(tblPersonalInfo, "Name:", txtName.Text);
                AddRow(tblPersonalInfo, "Date of Birth:", txtDOB.Text);
                AddRow(tblPersonalInfo, "Email:", txtEmail.Text);
                AddRow(tblPersonalInfo, "Phone:", txtPhone.Text);
                AddRow(tblPersonalInfo, "Address:", txtAddress.Text);
                foreach (Aspose.Pdf.Row rw in tblPersonalInfo.Rows)
                {
                    rw.MinRowHeight = 20;
                }
                //=========================== End of Personal Information Section ================================================
                //=========================== Skills Starts ===============================================================
                //add new heading...
                AddHeading(page, "Skills:", 10, true);
                // add text fragment...
                TextFragment txtFragSkills = new TextFragment();
                txtFragSkills.TextState.Font = FontRepository.FindFont("Calibri");
                txtFragSkills.TextState.FontSize = 8;
                txtFragSkills.Text = txtSkills.Text;
                txtFragSkills.TextState.LineSpacing = 5;
                //add text fragment in pagae paragraph...
                page.Paragraphs.Add(txtFragSkills);

                //=========================== End of Objective Statement Section ====================================================
                //============================ Section of Educational information Starts======================
                // Add a new heading ...
                AddHeading(page, "Educational Details:", 10, true);

                //create datatable...
                DataTable dtEducationalDetails = new DataTable();
                dtEducationalDetails.Columns.Add("Degree", typeof(string));
                dtEducationalDetails.Columns.Add("Total Marks/GPA", typeof(string));
                dtEducationalDetails.Columns.Add("Obtained Marks/CGPA", typeof(string));
                dtEducationalDetails.Columns.Add("Institute", typeof(string));


                //get data from the gridview and store in datatable...
                foreach (GridViewRow grow in gvEducationalDetails.Rows)
                {

                    TextBox txtDegree = grow.FindControl("txtDegree") as TextBox;
                    TextBox txtTotalMarks = grow.FindControl("txtTotalMarks") as TextBox;
                    TextBox txtObtainedMarks = grow.FindControl("txtObtainedMarks") as TextBox;
                    TextBox txtInstitute = grow.FindControl("txtInstitute") as TextBox;
                    if (txtDegree.Text.Trim() != "" && txtTotalMarks.Text.Trim() != "" && txtObtainedMarks.Text.Trim() != "" && txtInstitute.Text.Trim() != "")
                    {
                        DataRow drow = dtEducationalDetails.NewRow();
                        drow[0] = txtDegree.Text;
                        drow[1] = txtTotalMarks.Text;
                        drow[2] = txtObtainedMarks.Text;
                        drow[3] = txtInstitute.Text;
                        dtEducationalDetails.Rows.Add(drow);
                    }
                }

                //create table for personal information 
                Aspose.Pdf.Table tblEducationalInfo = new Aspose.Pdf.Table();
                 
                tblEducationalInfo.ColumnWidths = "100 100 100 100"; 
                //add table to the dataset
                DataSet ds = new DataSet();
                ds.Tables.Add(dtEducationalDetails);

                tblEducationalInfo.DefaultCellTextState.FontSize = 8;
                tblEducationalInfo.DefaultCellTextState.Font = FontRepository.FindFont("Calibri");
                 
                //Set the border style of the table...
                tblEducationalInfo.Border = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Black));
                // Set default cell border...
                tblEducationalInfo.DefaultCellBorder = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Black));
                // Set data source of the table...
                tblEducationalInfo.ImportDataTable(ds.Tables[0], true, 0, 0, ds.Tables[0].Rows.Count, 4);
                //Add table in paragraph...
                page.Paragraphs.Add(tblEducationalInfo);
                // Set the style of head row of the table...
                tblEducationalInfo.Rows[0].DefaultCellTextState.FontStyle = FontStyles.Bold;
                tblEducationalInfo.Rows[0].BackgroundColor = Aspose.Pdf.Color.LightGray; 
                //set the min height of the rows...
                foreach (Aspose.Pdf.Row rw in tblEducationalInfo.Rows)
                {
                    rw.Cells[3].IsWordWrapped = false;
                    rw.MinRowHeight = 15; 
                } 
                //=========================== End of Educational Information Section ================================================

                //============================ Section of Professional Experience Starts======================
                // Add a new heading ...
                AddHeading(page, "Employment History:", 10, true);

                //create a new datatbale to store the data...
                DataTable dtExperience = new DataTable();
                dtExperience.Columns.Add("Designation", typeof(string));
                dtExperience.Columns.Add("Duration", typeof(string));
                dtExperience.Columns.Add("Organization", typeof(string));


                // get the data from the grid view into datatable...
                foreach (GridViewRow grow in gvExperience.Rows)
                {

                    TextBox txtDesignation = grow.FindControl("txtDesignation") as TextBox;
                    TextBox txtDuration = grow.FindControl("txtDuration") as TextBox;
                    TextBox txtOrganization = grow.FindControl("txtOrganization") as TextBox;
                    if (txtDesignation.Text.Trim() != "" && txtDuration.Text.Trim() != "" && txtOrganization.Text.Trim() != "")
                    {
                        DataRow drow = dtExperience.NewRow();
                        drow[0] = txtDesignation.Text;
                        drow[1] = txtDuration.Text;
                        drow[2] = txtOrganization.Text;

                        dtExperience.Rows.Add(drow);
                    }
                }

                //create table for personal information 
                Aspose.Pdf.Table tblExperience = new Aspose.Pdf.Table();
                
               //set width of the columns...
                tblExperience.ColumnWidths = "100 100 200"; 
                //add table to the dataset
                ds = new DataSet();
                ds.Tables.Add(dtExperience);

                //set the font properties...
                tblExperience.DefaultCellTextState.FontSize = 8;
                tblExperience.DefaultCellTextState.Font = FontRepository.FindFont("Calibri");
                //Set the border style of the table...
                tblExperience.Border = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Black));
                // Set default cell border...
                tblExperience.DefaultCellBorder = new Aspose.Pdf.BorderInfo(Aspose.Pdf.BorderSide.All, .5f, Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Black));
                // Set data source of the table...
                tblExperience.ImportDataTable(ds.Tables[0], true, 0, 0, ds.Tables[0].Rows.Count, 3);
                //Add table in paragraph...
                page.Paragraphs.Add(tblExperience);
                // Set the style of head row of the table...
                tblExperience.Rows[0].DefaultCellTextState.FontStyle = FontStyles.Bold;
                tblExperience.Rows[0].BackgroundColor = Aspose.Pdf.Color.LightGray; 
                foreach (Aspose.Pdf.Row rw in tblExperience.Rows)
                {
                    rw.Cells[2].IsWordWrapped = false;
                    rw.MinRowHeight = 15;
                }
                //=========================== End of Professional Experience Section ================================================
                
                //=========================== Cover Letter Starts ===============================================================
                AddHeading(page, "Cover Letter:", 10, true);
                TextFragment txtFragCoverLetter = new TextFragment();
                txtFragCoverLetter.TextState.Font = FontRepository.FindFont("Calibri");
                txtFragCoverLetter.TextState.FontSize = 8;
                txtFragCoverLetter.Text = txtCoverLetter.Text;
                txtFragCoverLetter.TextState.LineSpacing = 5;
                page.Paragraphs.Add(txtFragCoverLetter); 

                //=========================== End of Cover Letter Section ====================================================

                //Add watermark in the document...
                foreach (Aspose.Pdf.Page pg in doc.Pages)
                {
                    AddWaterMark(pg);
                }

                string path = Server.MapPath("~/Uploads/Application_" + DateTime.Now.ToString("dd_MM_yy HH_mm_ss") + ".pdf");
                doc.Save(path);
                msg.Text = "<div class='alert alert-success'><button data-dismiss='alert' class='close' type='button'>×</button>Your application has been submitted successfully.</div>";
                //show message "Your application has been submitted successfully."

            }
            catch(Exception exp)
            {
                msg.Text = "<div class='alert alert-danger'><button data-dismiss='alert' class='close' type='button'>×</button>Exception Occured:" + exp.Message + "</div>";
            }
        }

        private void AddHeaderFooter(Document doc, string headerText, string footerText)
        {
            try
            {
                // Adding header and footer of the page...
                TextStamp headerTextStamp = new TextStamp(headerText);
                //set properties of the stamp
                headerTextStamp.TopMargin = 10;
                //textStamp.LeftMargin = 400;
                headerTextStamp.HorizontalAlignment = HorizontalAlignment.Center;
                headerTextStamp.VerticalAlignment = VerticalAlignment.Top;
                //add header on all pages
                foreach (Aspose.Pdf.Page pg in doc.Pages)
                {
                    pg.AddStamp(headerTextStamp);
                }

                //add foter on all pages (same as header)...
                
                foreach (Aspose.Pdf.Page pg in doc.Pages)
                {
                    TextStamp footerTextStamp = new TextStamp("Page No: " + pg.Number);
                    footerTextStamp.BottomMargin = 10;
                    footerTextStamp.HorizontalAlignment = HorizontalAlignment.Center;
                    footerTextStamp.VerticalAlignment = VerticalAlignment.Bottom;
                    pg.AddStamp(footerTextStamp);
                }
            }
            catch (Exception exp)
            {
                msg.Text = "<div class='alert alert-danger'><button data-dismiss='alert' class='close' type='button'>×</button>Exception Occured:" + exp.Message + "</div>";
            }
        }

        private void AddWaterMark(Aspose.Pdf.Page page)
        {
            try
            {
                //create e new artifact...
                Artifact art = new Artifact(Artifact.ArtifactType.Background, Artifact.ArtifactSubtype.Watermark);
                     
                //get the image to assign to watermark...
                FileStream imageStream = new FileStream(Server.MapPath("~/Images/aspose.jpg"), FileMode.Open);

                //add image to the resources of the page...
                page.Resources.Images.Add(imageStream);
                XImage ximage = page.Resources.Images[page.Resources.Images.Count];
                //set image of the watermark...
                art.SetImage(imageStream);
                //set the position of the watermark on the page...
                art.ArtifactHorizontalAlignment = HorizontalAlignment.Center;
                art.ArtifactVerticalAlignment = VerticalAlignment.Center; 
                //wart.Rotation = 45; //to rotate the watermark...
                art.Opacity = 0.1; 
                
                //add watermark in page 
                page.Artifacts.Add(art);
            }
            catch (Exception exp)
            {
                msg.Text = "<div class='alert alert-danger'><button data-dismiss='alert' class='close' type='button'>×</button>Exception Occured:" + exp.Message + "</div>";
            }
        }
         
        protected void AddRow(Aspose.Pdf.Table tblInfo, string strLabel, string strValue)
        {
            try
            {
                //create a new row...
                Aspose.Pdf.Row row = tblInfo.Rows.Add();

                //create a new cell...
                Aspose.Pdf.Cell cellLabel = row.Cells.Add();
                //add value in cell...
                cellLabel.Paragraphs.Add(new TextFragment(strLabel));
                //set font properties...
                cellLabel.DefaultCellTextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Calibri");
                cellLabel.DefaultCellTextState.FontSize = 8;
                cellLabel.DefaultCellTextState.FontStyle = FontStyles.Bold;
                //add new cell in row...
                Aspose.Pdf.Cell cell = row.Cells.Add();
                //add value in the cell...
                cell.Paragraphs.Add(new TextFragment(strValue));
                cell.DefaultCellTextState.Font = Aspose.Pdf.Text.FontRepository.FindFont("Calibri");
                cell.DefaultCellTextState.FontSize = 8;

            }
            catch (Exception exp)
            {
                msg.Text = "<div class='alert alert-danger'><button data-dismiss='alert' class='close' type='button'>×</button>Exception Occured:" + exp.Message + "</div>";
            }
        }

        protected void AddHeading(Aspose.Pdf.Page page, string strValue, float fontSize, bool IsUnderline)
        {
            try
            {
                //add a empty line in the page...
                page.Paragraphs.Add(new TextFragment(" "));
                //create a new text fragment...
                TextFragment txtHeading1 = new TextFragment(strValue);
                txtHeading1.TextState.FontSize = fontSize;
                txtHeading1.TextState.FontStyle = FontStyles.Bold;
                if (IsUnderline)
                {
                    txtHeading1.TextState.Underline = true;
                }
                //add text fragment in the paragraph of the page...
                page.Paragraphs.Add(txtHeading1);
                //add empty line in the page...
                page.Paragraphs.Add(new TextFragment(" "));
            }
            catch (Exception exp)
            {
                msg.Text = "<div class='alert alert-danger'><button data-dismiss='alert' class='close' type='button'>×</button>Exception Occured:" + exp.Message + "</div>";
            }
        }

        protected void btnMergeAllFiles_Click(object sender, EventArgs e)
        {
            try
            {
                //delete file if already exists...
                if (File.Exists(Server.MapPath("~/Uploads/Catalog.pdf")))
                {
                    File.Delete(Server.MapPath("~/Uploads/Catalog.pdf"));
                }
                // create new document to save the catalog...
                Document doc = new Document();

                //get all the applications in the directory
                string[] pdfFiles = Directory.GetFiles(Server.MapPath("~/Uploads"), "*.pdf")
                       .Select(path => Path.GetFileName(path))
                       .ToArray();
                if (pdfFiles.Count() == 0)
                {
                    msg.Text = "<div class='alert alert-danger'><button data-dismiss='alert' class='close' type='button'>×</button>There is currently no application available.</div>";
                    return;
                }
                else
                {
                    foreach (string val in pdfFiles)
                    {
                        //get the pdf document.. 
                        Document application = new Document(Server.MapPath("~/Uploads/" + val));
                        //merge the pages in catalog document...
                        doc.Pages.Add(application.Pages);
                    }
                    //====================================== Adding Table of Content Starts =========================================
                    //add TOC page...
                    Aspose.Pdf.Page tocPage = doc.Pages.Insert(1);
                    //add watermark on table of content  
                    AddWaterMark(tocPage);
                    // Create object to represent TOC information
                    TocInfo tocInfo = new TocInfo();
                    TextFragment title = new TextFragment("Table Of Contents");
                    title.TextState.FontSize = 20;
                    title.TextState.FontStyle = FontStyles.Bold;

                    // Set the title for TOC
                    tocInfo.Title = title;
                    tocPage.TocInfo = tocInfo;


                    for (int i = 0; i < pdfFiles.Count(); i++)
                    {
                        // Create Heading object
                        Aspose.Pdf.Heading heading2 = new Aspose.Pdf.Heading(1);
                        TextSegment segment2 = new TextSegment();
                        heading2.TocPage = tocPage;
                        heading2.Segments.Add(segment2);

                        // Specify the destination page for heading object
                        heading2.DestinationPage = doc.Pages[i + 2];

                        // Destination page
                        heading2.Top = doc.Pages[i + 2].Rect.Height;

                        // Destination coordinate
                        segment2.Text = "Application Form No. " + (i + 1).ToString();

                        // Add heading to page containing TOC
                        tocPage.Paragraphs.Add(heading2);
                    }
                    //===================================== TOC Ends ==========================================
                    
                    //save the final catalog file...
                    string catalogpath = Server.MapPath("~/Catalogs/Catalog.pdf");
                    doc.Save(catalogpath);
                    msg.Text = "<div class='alert alert-success'><button data-dismiss='alert' class='close' type='button'>×</button>Applications have been saved in catalog.</div>";
                    //show success message
                }
            }
            catch (Exception exp)
            {
                msg.Text = "<div class='alert alert-danger'><button data-dismiss='alert' class='close' type='button'>×</button>Exception Occured:" + exp.Message + "</div>";
            }

        }
    }
}
