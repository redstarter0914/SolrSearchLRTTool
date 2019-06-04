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
    public partial class FrmKeyWord : Form
    {
        public FrmKeyWord()
        {
            InitializeComponent();
        }

        private void btnCreateKeyWord_Click(object sender, EventArgs e)
        {

            var keyword = this.txtKey.Text;
            var ss = GetKeyByQuotes(keyword);

            var sd= keyword.ReplaceALLByKeyword();

            this.txtResult.Text = ss;
            //keyword.ReplaceALLByKeyword();
            this.txtResult.Refresh();
        }

        private string GetKeyByQuotes(string keyword)
        {
            // string result = keyword;
            List<ReplaceResult> diclist = new List<ReplaceResult>();

            var dlist = keyword.Split(' ');
            for (int d = 0; d < dlist.Count(); d++)
            {
                var str = dlist[d];                 
                var rstr = str.ReplaceALLByKeyword(); 
                var nstr = rstr.Replace("\"","").Trim();
                var rltstr = "";
                if (str.Contains("(") || str.Contains(")") || str.Contains("（") || str.Contains("）"))
                {
                    if (!string.IsNullOrEmpty(rstr))
                    {
                        rltstr = str.Replace(nstr, rstr);
                    }
                    else
                    {
                        rltstr = rstr;
                    }
                }
                else
                {
                    rltstr = rstr;
                }

                diclist.Add(new ReplaceResult { id = d, Newstr = rltstr, oldstr = str });
            }

            string result = "";
            if (diclist != null && diclist.Count() > 0)
            {
                diclist = diclist.OrderBy(q => q.id).ToList();
                foreach (var rd in diclist)
                {
                    if (!string.IsNullOrEmpty(rd.Newstr))
                    {
                        result = result + " " + rd.Newstr;
                    }
                    else
                    {
                        result = result + " " + rd.oldstr;
                    }
                }
            } 
            return result;
        }

    }

    public class ReplaceResult
    {
        public int id { get; set; }
        public string oldstr { get; set; }
        public string Newstr { get; set; }
    }
}
