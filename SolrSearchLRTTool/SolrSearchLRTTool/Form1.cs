using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolrSearchLRTTool
{
    public partial class formLTRScore : Form
    {
        public formLTRScore()
        {
            InitializeComponent();
        }

        private void btnCreateData_Click(object sender, EventArgs e)
        {
            this.btnCreateData.Enabled = false;
            this.tsbtnCreateData.Enabled = false;
            if (this.cbxType.SelectedItem == null)
            {
                this.btnCreateData.Enabled = true;
                this.tsbtnCreateData.Enabled = true;
                MessageBox.Show("请选择打分方式");
                return;
            }
            if (string.IsNullOrEmpty(this.txtUrl.Text))
            {
                this.btnCreateData.Enabled = true;
                this.tsbtnCreateData.Enabled = true;
                MessageBox.Show("请输入SolrUrl");
                return;
            }
            if (string.IsNullOrEmpty(this.txtKey.Text))
            {
                this.btnCreateData.Enabled = true;
                this.tsbtnCreateData.Enabled = true;
                MessageBox.Show("请输入检索关键字");
                return;
            }
            if (this.cbxSearchType.SelectedItem == null)
            {
                this.btnCreateData.Enabled = true;
                this.tsbtnCreateData.Enabled = true;
                MessageBox.Show("请选择查询列名");
                return;
            }
            SearchModel smodel = new SearchModel();
            smodel.SearchKey = this.txtKey.Text;
            smodel.SearchNum = (int)this.numCreateNum.Value;
            smodel.SearchStart = (int)this.numStart.Value;
            smodel.SearchType = this.cbxSearchType.SelectedItem.ToString();
            smodel.Url = this.txtUrl.Text;

            QueryModel model = new QueryModel();
            model.SearchModel = smodel;
            model.Score = this.numScore.Value;
            model.ScoreType = this.cbxType.SelectedItem.ToString();

            var result = SearchData.SearchResult(smodel);

            List<ExportDataModel> list = new List<ExportDataModel>();
            if (result != null)
            {
                if (result.Response != null && result.Response.Docs.Count > 0)
                {
                    //int j = result.Response.Docs.Count;
                    foreach (var d in result.Response.Docs)
                    {
                        ExportDataModel edm = new ExportDataModel();
                        edm.DocID = d.Id;

                        //edm.Score = j - result.Response.Docs.IndexOf(d);
                        if (model.Score > 0)
                        {
                            edm.Score = model.Score;
                        }
                        else
                        {
                            edm.Score = null;
                        }
                        edm.ScoreType = model.ScoreType;
                        edm.SearchKey = smodel.SearchKey;

                        list.Add(edm);

                        lblnum.Text = (result.Response.Docs.IndexOf(d) + 1).ToString() + "条";
                        lblnum.Refresh();
                    }
                }
            }
            else
            {
                this.btnCreateData.Enabled = true;
                this.tsbtnCreateData.Enabled = true;
                MessageBox.Show("查询失败");
                return;
            }

            if (list.Count > 0)
            {
                List<ExportObj> head = new List<ExportObj>();
                head.Add(new ExportObj { DataType = typeof(string), key = "SearchKey", Name = "ScoreType" });
                head.Add(new ExportObj { DataType = typeof(string), key = "DocID", Name = "DocID" });
                head.Add(new ExportObj { DataType = typeof(decimal), key = "Score", Name = "Score" });
                head.Add(new ExportObj { DataType = typeof(string), key = "ScoreType", Name = "ScoreType" });

                var rootpath = AppDomain.CurrentDomain.BaseDirectory + "/Export/" + smodel.SearchType;

                string filename = NPOIExcelHelper.ExportListToExcelNew(list.AsQueryable(), head, "ExportScoreData", rootpath);
                if (!string.IsNullOrEmpty(filename))
                {
                    MessageBox.Show("生成成功");
                    this.txtKey.Clear();

                    this.btnCreateData.Enabled = true;
                    this.tsbtnCreateData.Enabled = true;
                    return;
                }
                else
                {
                    this.btnCreateData.Enabled = true;
                    this.tsbtnCreateData.Enabled = true;
                    MessageBox.Show("生成失败");
                    return;
                }
            }
            else
            {

                this.btnCreateData.Enabled = true;
                this.tsbtnCreateData.Enabled = true;
                MessageBox.Show("未查询到数据");
                return;
            }
        }

        private void tsbtnCreateData_Click(object sender, EventArgs e)
        {

            this.btnCreateData.Enabled = false;
            this.tsbtnCreateData.Enabled = false;

            List<QueryExcelModel> KeyList = new List<QueryExcelModel>();
            var readpath = AppDomain.CurrentDomain.BaseDirectory + "/UploadData/uploaddata.xlsx";
            var data = NPOIExcelHelper.ExcelToDataTable(readpath, "Sheet1");
            if (data != null && data.Rows.Count > 0)
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    KeyList.Add(new QueryExcelModel()
                    {
                        QueryID = Convert.ToInt32(data.Rows[i]["QueryID"].ToString()),
                        QueryString = data.Rows[i]["QueryString"].ToString()
                    });
                }
            }
            else
            {
                this.btnCreateData.Enabled = true;
                this.tsbtnCreateData.Enabled = true;
                MessageBox.Show("模板数据为空");
                return;
            }
            if (this.cbxType.SelectedItem == null)
            {
                this.btnCreateData.Enabled = true;
                this.tsbtnCreateData.Enabled = true;
                MessageBox.Show("请选择打分方式");
                return;
            }
            if (string.IsNullOrEmpty(this.txtUrl.Text))
            {
                this.btnCreateData.Enabled = true;
                this.tsbtnCreateData.Enabled = true;
                MessageBox.Show("请输入SolrUrl");
                return;
            }
            if (this.cbxSearchType.SelectedItem == null)
            {
                this.btnCreateData.Enabled = true;
                this.tsbtnCreateData.Enabled = true;
                MessageBox.Show("请选择查询列名");
                return;
            }

            List<QueryExcelModelNoData> KeyListSearchSuccess = new List<QueryExcelModelNoData>();
            List<QueryExcelModelNoData> KeyListSearchError = new List<QueryExcelModelNoData>();

            foreach (var kl in KeyList)
            {
                var rootpath = AppDomain.CurrentDomain.BaseDirectory + "/Export/ALL";
                SearchModel smodel = new SearchModel();
                smodel.SearchKey = kl.QueryString;
                smodel.SearchNum = (int)this.numCreateNum.Value;
                smodel.SearchStart = (int)this.numStart.Value;
                smodel.SearchType = this.cbxSearchType.SelectedItem.ToString();
                smodel.Url = this.txtUrl.Text;

                QueryModel model = new QueryModel();
                model.SearchModel = smodel;
                model.Score = this.numScore.Value;
                model.ScoreType = this.cbxType.SelectedItem.ToString();

                var result = SearchData.SearchResult(smodel);

                List<ExportDataModel> list = new List<ExportDataModel>();
                if (result != null)
                {
                    if (result.Response != null && result.Response.Docs.Count > 0)
                    {
                        int j = result.Response.Docs.Count + 2;
                        foreach (var d in result.Response.Docs)
                        {
                            ExportDataModel edm = new ExportDataModel();
                            edm.DocID = d.Id;
                            edm.Score = j - result.Response.Docs.IndexOf(d);
                            //if (model.Score > 0)
                            //{
                            //    edm.Score = model.Score;
                            //}
                            //else
                            //{
                            //    edm.Score = null;
                            //}
                            edm.ScoreType = model.ScoreType;
                            edm.SearchKey = smodel.SearchKey;

                            list.Add(edm);
                        }
                    }
                }
                else
                {
                    KeyListSearchError.Add(new QueryExcelModelNoData
                    {
                        ErrorType = "SearchError",
                        QueryID = kl.QueryID,
                        QueryString = kl.QueryString
                    });
                }

                if (list.Count > 0)
                {
                    List<ExportObj> head = new List<ExportObj>();
                    head.Add(new ExportObj { DataType = typeof(string), key = "SearchKey", Name = "ScoreType" });
                    head.Add(new ExportObj { DataType = typeof(string), key = "DocID", Name = "DocID" });
                    head.Add(new ExportObj { DataType = typeof(decimal), key = "Score", Name = "Score" });
                    head.Add(new ExportObj { DataType = typeof(string), key = "ScoreType", Name = "ScoreType" });

                    var rpath = rootpath + "/" + smodel.SearchType;

                    string filename = NPOIExcelHelper.ExportListToExcelNew(list.AsQueryable(), head, "ExportScoreData", rpath, kl.QueryID.ToString());
                    if (!string.IsNullOrEmpty(filename))
                    {
                        KeyListSearchSuccess.Add(new QueryExcelModelNoData
                        {
                            ErrorType = "SearchSuccess",
                            QueryID = kl.QueryID,
                            QueryString = kl.QueryString
                        });
                    }
                    else
                    {
                        KeyListSearchError.Add(new QueryExcelModelNoData
                        {
                            ErrorType = "CreateError",
                            QueryID = kl.QueryID,
                            QueryString = kl.QueryString
                        });
                    }
                }
                else
                {
                    KeyListSearchError.Add(new QueryExcelModelNoData
                    {
                        ErrorType = "NoData",
                        QueryID = kl.QueryID,
                        QueryString = kl.QueryString
                    });
                }

                lblnum.Text = (KeyList.IndexOf(kl) + 1).ToString() + "条";
                lblnum.Refresh();
            }

            if (KeyListSearchError != null && KeyListSearchError.Count > 0)
            {
                KeyListSearchError = KeyListSearchError.OrderBy(p => p.ErrorType).ThenBy(p => p.QueryID).ToList();
                List<ExportObj> head1 = new List<ExportObj>();
                head1.Add(new ExportObj { DataType = typeof(string), key = "ErrorType", Name = "ErrorType" });
                head1.Add(new ExportObj { DataType = typeof(int), key = "QueryID", Name = "QueryID" });
                head1.Add(new ExportObj { DataType = typeof(string), key = "QueryString", Name = "QueryString" });

                var errorpath = AppDomain.CurrentDomain.BaseDirectory + "/ErrorData";
                string filename1 = NPOIExcelHelper.ExportListToExcelNew(KeyListSearchError.AsQueryable(), head1, "KeyListSearchError", errorpath);

            }

            this.btnCreateData.Enabled = true;
            this.tsbtnCreateData.Enabled = true;
            string msg = string.Format("生成成功 {0} 条，生成失败 {1} 条", KeyListSearchSuccess.Count, KeyListSearchError.Count);
            MessageBox.Show(msg);

        }

        private void tsbtnExchangeKeyWord_Click(object sender, EventArgs e)
        {
            FrmKeyWord frm = new FrmKeyWord();
            frm.Show();
        }
    }
}
