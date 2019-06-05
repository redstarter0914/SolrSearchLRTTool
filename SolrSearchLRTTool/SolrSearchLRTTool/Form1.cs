using SolrSearchLRTTool.Models;
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
            checkSearch();

            SearchModel smodel = GetSearchModel();
            QueryModel model = GetQueryModel();

            List<string> querySuccessKey = new List<string>();
            List<string> queryErrorKey = new List<string>();

            List<string> KeywordList = new List<string>();
            KeywordList = this.txtKey.Text.Trim().Split(',').ToList();
            if (KeywordList != null && KeywordList.Count > 0)
            {

                List<QueryExcelModelNoData> errorResult = new List<QueryExcelModelNoData>();
                List<ExportDataModel> searchResultData = new List<ExportDataModel>();

                var ExportQueryExcelModel = new QueryExcelModel() { QueryString = "AllKey", QueryID = 1, QueryType = this.txtKey.Text.Trim() };
                foreach (var key in KeywordList)
                {
                    smodel.SearchKey = key;
                    model.SearchModel = smodel;

                    var QueryExcelModel = new QueryExcelModel() { QueryString = key, QueryID = KeywordList.IndexOf(key), QueryType = key };

                    var data = GetExportDatas(model, QueryExcelModel);
                    searchResultData.AddRange(data.searchData);

                    if (data.queryResultList.Count > 0)
                    {
                        errorResult.AddRange(data.queryResultList.Where(q => q.ErrorType != EnumErrorType.querySuccess.ToString()));
                    }

                    if (data.searchData.Count > 0)
                    {
                        querySuccessKey.Add(key);
                    }
                    else
                    {
                        queryErrorKey.Add(key);
                    }

                    lblnum.Text = string.Format(" {0} ：{1}", KeywordList.IndexOf(key), key);
                    lblnum.Refresh();
                }

                if (searchResultData.Count > 0)
                {
                    var searchResult = ExportDataToExcel(searchResultData, model, ExportQueryExcelModel, true);
                    if (searchResult.Count > 0)
                    {
                        errorResult.AddRange(searchResult.Where(q => q.ErrorType != EnumErrorType.CreateFileSuccess.ToString()));
                    }
                }
                if (errorResult.Count > 0)
                {
                    errorlog(errorResult);
                }

                this.txtKeyResult.Text = string.Format(@"总数：{0} 条   Key 成功：{1}    失败：{2}", searchResultData.Count.ToString(), string.Join(" , ", querySuccessKey), string.Join(" , ", queryErrorKey));
                this.txtKeyResult.Refresh();
            }

            this.btnCreateData.Enabled = true;
            this.tsbtnCreateData.Enabled = true;
            this.tslSingleBtn.Enabled = true;

        }

        private void tsbtnCreateData_Click(object sender, EventArgs e)
        {
            checkSearch();
            List<QueryExcelModel> KeyList = checkTemplate();              

            List<ExportDataModel> list = new List<ExportDataModel>();
            SearchModel smodel = GetSearchModel();
            QueryModel model = GetQueryModel();

            var queryTypeList = KeyList.Select(q => q.QueryType).Distinct();

            List<QueryExcelModelNoData> errorResult = new List<QueryExcelModelNoData>();
            Dictionary<string,int> querySuccessKey = new Dictionary<string, int>();
            Dictionary<string, int> queryErrorKey = new Dictionary<string, int>();

            foreach (var qt in queryTypeList)
            {
                List<ExportDataModel> searchResultData = new List<ExportDataModel>();
                var queryKeyList = KeyList.Where(q => q.QueryType == qt).ToList();
                foreach (var kl in queryKeyList)
                {
                    lblnum.Text = string.Format("{0} ：{1}", KeyList.IndexOf(kl).ToString(), kl.QueryString);
                    lblnum.Refresh();
                    smodel.SearchKey = kl.QueryString;
                    model.SearchModel = smodel;

                    var data = GetExportDatas(model, kl);
                    searchResultData.AddRange(data.searchData);
                    if (data.queryResultList.Count > 0)
                    {
                        errorResult.AddRange(data.queryResultList.Where(q => q.ErrorType != EnumErrorType.querySuccess.ToString()));
                    }
                    if (data.searchData.Count > 0)
                    {
                        querySuccessKey.Add(kl.QueryString, data.searchData.Count);
                    }
                    else
                    {

                    }


                }
            }





            foreach (var kl in KeyList)
            {
                smodel.SearchKey = kl.QueryString;
                model.SearchModel = smodel;



                var result = SearchData.SearchResult(smodel);

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
                            edm.QueryType = kl.QueryType;

                            list.Add(edm);
                        }
                    }
                    else
                    {
                        KeyListSearchSuccess.Add(new QueryExcelModelNoData
                        {
                            ErrorType = "SearchSuccess",
                            QueryID = kl.QueryID,
                            QueryString = kl.QueryString,
                            QueryType = kl.QueryType
                        });
                    }
                }
                else
                {
                    KeyListSearchError.Add(new QueryExcelModelNoData
                    {
                        ErrorType = "SearchError",
                        QueryID = kl.QueryID,
                        QueryString = kl.QueryString,
                        QueryType = kl.QueryType

                    });
                }

              
            }

            if (list.Count > 0)
            {
                List<ExportObj> head = new List<ExportObj>();
                head.Add(new ExportObj { DataType = typeof(string), key = "SearchKey", Name = "ScoreType" });
                head.Add(new ExportObj { DataType = typeof(string), key = "DocID", Name = "DocID" });
                head.Add(new ExportObj { DataType = typeof(decimal), key = "Score", Name = "Score" });
                head.Add(new ExportObj { DataType = typeof(string), key = "ScoreType", Name = "ScoreType" });


                var queryTypeList = list.Select(q => q.QueryType).Distinct().ToList();
                foreach (var qt in queryTypeList)
                {
                    var rpath = rootpath + "/" + smodel.SearchType;
                    var datalist = list.Where(q => q.QueryType == qt);
                    string filename = NPOIExcelHelper.ExportListToExcelNew(datalist.AsQueryable(), head, "ExportScoreData", rpath, qt);
                    if (!string.IsNullOrEmpty(filename))
                    {
                        KeyListSearchSuccess.Add(new QueryExcelModelNoData
                        {
                            ErrorType = "CreateSuccess",
                            //QueryID = kl.QueryID,
                            // QueryString = kl.QueryString,
                            QueryType = qt
                        });

                        this.txtKeyResult.Text = string.Format("查询类别：{0}  文件名称：{1}", qt, filename);
                        this.txtKeyResult.Refresh();
                    }
                    else
                    {
                        KeyListSearchError.Add(new QueryExcelModelNoData
                        {
                            ErrorType = "CreateError",
                            // QueryID = kl.QueryID,
                            //QueryString = kl.QueryString,
                            QueryType = qt
                        });
                    }
                }
            }
            else
            {
                KeyListSearchError.Add(new QueryExcelModelNoData
                {
                    ErrorType = "AllTemple Search NoData",
                    //QueryID = kl.QueryID,
                    //QueryString = kl.QueryString,
                    //QueryType = kl.QueryType
                });
            }

            errorlog(KeyListSearchError);

            this.btnCreateData.Enabled = true;
            this.tsbtnCreateData.Enabled = true;
            string msg = string.Format("生成成功 {0} 条，生成失败 {1} 条", KeyListSearchSuccess.Count, KeyListSearchError.Count);
            this.txtKeyResult.Text = msg;
            this.txtKeyResult.Refresh();
            // MessageBox.Show(msg);

        }

        private void tsbtnExchangeKeyWord_Click(object sender, EventArgs e)
        {
            FrmKeyWord frm = new FrmKeyWord();
            frm.Show();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

            checkSearch();
            List<QueryExcelModel> KeyList = checkTemplate();

            List<QueryExcelModelNoData> KeyListSearchSuccess = new List<QueryExcelModelNoData>();
            List<QueryExcelModelNoData> KeyListSearchError = new List<QueryExcelModelNoData>();

            var rootpath = AppDomain.CurrentDomain.BaseDirectory + "/Export/ALL";


            List<ExportDataModel> list = new List<ExportDataModel>();
            SearchModel smodel = GetSearchModel();

            QueryModel model = GetQueryModel();

            foreach (var kl in KeyList)
            {
                smodel.SearchKey = kl.QueryString;
                model.SearchModel = smodel;
                model.QueryType = kl.QueryType;

                var result = SearchData.SearchResult(smodel);

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
                            edm.QueryType = kl.QueryType;

                            list.Add(edm);
                        }
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
                                ErrorType = "CreateSuccess",
                                QueryID = kl.QueryID,
                                QueryString = kl.QueryString
                            });

                            this.txtKeyResult.Text = string.Format("序号：{0}  文件名称：{1}", kl.QueryID.ToString(), filename);
                            this.txtKeyResult.Refresh();
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
                            ErrorType = "AllTemple Search NoData",
                            QueryID = kl.QueryID,
                            QueryString = kl.QueryString
                        });
                    }

                }
                else
                {
                    KeyListSearchError.Add(new QueryExcelModelNoData
                    {
                        ErrorType = "SearchError",
                        QueryID = kl.QueryID,
                        QueryString = kl.QueryString,
                        QueryType = kl.QueryType

                    });
                }

                lblnum.Text = string.Format("序号：{0}  关键字：{1}", KeyList.IndexOf(kl).ToString(), kl.QueryString);
                lblnum.Refresh();
            }

            errorlog(KeyListSearchError);

            this.btnCreateData.Enabled = true;
            this.tsbtnCreateData.Enabled = true;
            string msg = string.Format("生成成功 {0} 条，生成失败 {1} 条", KeyListSearchSuccess.Count, KeyListSearchError.Count);
            this.txtKeyResult.Text = msg;
            this.txtKeyResult.Refresh();
        }

        private void checkSearch()
        {
            this.btnCreateData.Enabled = false;
            this.tsbtnCreateData.Enabled = false;
            this.tslSingleBtn.Enabled = false;

            this.txtKeyResult.Text = "";
            this.txtKeyResult.Refresh();
            if (this.cbxType.SelectedItem == null)
            {
                this.btnCreateData.Enabled = true;
                this.tsbtnCreateData.Enabled = true;
                this.tslSingleBtn.Enabled = true;
                MessageBox.Show("请选择打分方式");
                return;
            }
            if (string.IsNullOrEmpty(this.txtUrl.Text))
            {
                this.btnCreateData.Enabled = true;
                this.tsbtnCreateData.Enabled = true;
                this.tslSingleBtn.Enabled = true;
                MessageBox.Show("请输入SolrUrl");
                return;
            }
            if (string.IsNullOrEmpty(this.txtKey.Text))
            {
                this.btnCreateData.Enabled = true;
                this.tsbtnCreateData.Enabled = true;
                this.tslSingleBtn.Enabled = true;
                MessageBox.Show("请输入检索关键字");
                return;
            }
            if (this.cbxSearchType.SelectedItem == null)
            {
                this.btnCreateData.Enabled = true;
                this.tsbtnCreateData.Enabled = true;
                this.tslSingleBtn.Enabled = true;
                MessageBox.Show("请选择查询列名");
                return;
            }
        }

        private List<QueryExcelModel> checkTemplate()
        {
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
                        QueryString = data.Rows[i]["QueryString"].ToString(),
                        QueryType = data.Rows[i]["QueryType"].ToString()
                    });
                }
            }
            else
            {
                this.btnCreateData.Enabled = true;
                this.tsbtnCreateData.Enabled = true;
                this.tslSingleBtn.Enabled = true;
                MessageBox.Show("模板数据为空");
                return KeyList;
            }
            return KeyList;
        }

        private void errorlog(List<QueryExcelModelNoData> KeyListSearchError)
        {
            if (KeyListSearchError != null && KeyListSearchError.Count > 0)
            {
                KeyListSearchError = KeyListSearchError.OrderBy(p => p.ErrorType).ThenBy(p => p.QueryID).ToList();
                List<ExportObj> head1 = new List<ExportObj>();
                head1.Add(new ExportObj { DataType = typeof(string), key = "ErrorType", Name = "ErrorType" });
                head1.Add(new ExportObj { DataType = typeof(int), key = "QueryID", Name = "QueryID" });
                head1.Add(new ExportObj { DataType = typeof(string), key = "QueryString", Name = "QueryString" });
                head1.Add(new ExportObj { DataType = typeof(string), key = "QueryType", Name = "QueryType" });

                var errorpath = AppDomain.CurrentDomain.BaseDirectory + "/ErrorData";
                string filename1 = NPOIExcelHelper.ExportListToExcelNew(KeyListSearchError.AsQueryable(), head1, "KeyListSearchError", errorpath);
            }
        }

        private SearchModel GetSearchModel()
        {
            SearchModel smodel = new SearchModel();

            smodel.SearchNum = (int)this.numCreateNum.Value;
            smodel.SearchStart = (int)this.numStart.Value;
            smodel.SearchType = this.cbxSearchType.SelectedItem.ToString();
            smodel.Url = this.txtUrl.Text;

            return smodel;
        }

        private QueryModel GetQueryModel()
        {
            QueryModel model = new QueryModel();

            model.Score = this.numScore.Value;
            model.ScoreType = this.cbxType.SelectedItem.ToString();

            return model;
        }

        private SearchDataModel GetExportDatas(QueryModel model, QueryExcelModel keyModel)
        {
            SearchDataModel searchDataModel = new SearchDataModel();
            List<QueryExcelModelNoData> resultKeyList = new List<QueryExcelModelNoData>();
            List<ExportDataModel> list = new List<ExportDataModel>();
            var result = SearchData.SearchResult(model.SearchModel);
            if (result != null)
            {
                if (result.Response != null && result.Response.Docs.Count > 0)
                {
                    foreach (var d in result.Response.Docs)
                    {
                        ExportDataModel edm = new ExportDataModel();
                        edm.DocID = d.Id;
                        if (model.Score > 0)
                        {
                            edm.Score = model.Score;
                        }
                        else
                        {
                            edm.Score = null;
                        }
                        edm.ScoreType = model.ScoreType;
                        edm.SearchKey = model.SearchModel.SearchKey;
                        edm.QueryType = model.QueryType;

                        list.Add(edm);
                    }
                    resultKeyList.Add(new QueryExcelModelNoData()
                    {
                        ErrorType = EnumErrorType.querySuccess.ToString(),
                        QueryString = keyModel.QueryString,
                        QueryID = keyModel.QueryID,
                        QueryType = keyModel.QueryType
                    });
                }
                else
                {
                    resultKeyList.Add(new QueryExcelModelNoData()
                    {
                        ErrorType = EnumErrorType.SearchNoData.ToString(),
                        QueryString = keyModel.QueryString,
                        QueryID = keyModel.QueryID,
                        QueryType = keyModel.QueryType
                    });
                }
            }
            else
            {
                resultKeyList.Add(new QueryExcelModelNoData()
                {
                    ErrorType = EnumErrorType.SearchError.ToString(),
                    QueryString = keyModel.QueryString,
                    QueryID = keyModel.QueryID,
                    QueryType = keyModel.QueryType
                });
            }

            searchDataModel.searchData = list;

            searchDataModel.queryResultList = resultKeyList;

            return searchDataModel;
        }

        private List<QueryExcelModelNoData> ExportDataToExcel(List<ExportDataModel> list, QueryModel model, QueryExcelModel keymodel, bool IsSingle = false, bool AllSingle = false)
        {
            List<QueryExcelModelNoData> KeyListSearch = new List<QueryExcelModelNoData>();
            if (list.Count > 0)
            {
                List<ExportObj> head = new List<ExportObj>();
                head.Add(new ExportObj { DataType = typeof(string), key = "SearchKey", Name = "ScoreType" });
                head.Add(new ExportObj { DataType = typeof(string), key = "DocID", Name = "DocID" });
                head.Add(new ExportObj { DataType = typeof(decimal), key = "Score", Name = "Score" });
                head.Add(new ExportObj { DataType = typeof(string), key = "ScoreType", Name = "ScoreType" });

                string filename = "";
                var rootpath = AppDomain.CurrentDomain.BaseDirectory + "/Export/ALL/" + model.SearchModel.SearchType;
                if (IsSingle)
                {
                    rootpath = AppDomain.CurrentDomain.BaseDirectory + "/Export/" + model.SearchModel.SearchType;
                    filename = NPOIExcelHelper.ExportListToExcelNew(list.AsQueryable(), head, "ExportScoreData", rootpath);
                }
                else
                {
                    if (AllSingle)
                    {
                        filename = NPOIExcelHelper.ExportListToExcelNew(list.AsQueryable(), head, "ExportScoreData", rootpath, keymodel.QueryID.ToString());
                    }
                    else
                    {
                        filename = NPOIExcelHelper.ExportListToExcelNew(list.AsQueryable(), head, "ExportScoreData", rootpath, keymodel.QueryType.ToString());
                    }
                }

                if (!string.IsNullOrEmpty(filename))
                {
                    KeyListSearch.Add(new QueryExcelModelNoData
                    {
                        ErrorType = EnumErrorType.CreateFileSuccess.ToString(),//"CreateSuccess",
                        QueryID = keymodel.QueryID,
                        QueryString = keymodel.QueryString,
                        QueryType = keymodel.QueryType
                    });

                    this.txtKeyResult.Text = string.Format("序号：{0}  文件名称：{1}", keymodel.QueryID.ToString(), filename);
                    this.txtKeyResult.Refresh();
                }
                else
                {
                    KeyListSearch.Add(new QueryExcelModelNoData
                    {
                        ErrorType = EnumErrorType.CreateFileError.ToString(), //"CreateError",
                        QueryID = keymodel.QueryID,
                        QueryString = keymodel.QueryString,
                        QueryType = keymodel.QueryType
                    });
                }
            }
            else
            {
                KeyListSearch.Add(new QueryExcelModelNoData
                {
                    ErrorType = EnumErrorType.SearchNoData.ToString(),// "AllTemple Search NoData",
                    QueryID = keymodel.QueryID,
                    QueryString = keymodel.QueryString,
                    QueryType = keymodel.QueryType
                });
            }

            return KeyListSearch;
        }

    }
}
