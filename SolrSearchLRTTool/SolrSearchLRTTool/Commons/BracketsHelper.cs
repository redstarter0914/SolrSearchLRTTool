using System.Collections.Generic;
using System.Linq;

namespace SolrSearchLRTTool
{
    public class BracketsHelper
    {
        /// <summary>
        /// 获取括号里内容
        /// </summary>
        /// <param name="sInput"></param>
        /// <returns></returns>
        public List<BracketContent> GetResultList(string sInput)
        {
            List<BracketContent> Result = new List<BracketContent>();
            char[] arr = sInput.ToArray();

            //记录括号的位置
            List<Brackets> listBrackets = new List<Brackets>();
            //左括号个数
            int iLeftNum = 0;
            //右括号个数
            int iRightNum = 0;
            //括号最大层数
            int iMaxDeep = 0;
            bool bIsErrFormat = false;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == FastConstHelper.CMF_Bracket)
                {
                    iLeftNum++;
                    listBrackets.Add(new Brackets(true, i, iLeftNum));
                }
                else if (arr[i] == FastConstHelper.CME_Bracket)
                {
                    if (iLeftNum - iRightNum > iMaxDeep)
                    {
                        iMaxDeep = iLeftNum - iRightNum;
                    }
                    iRightNum++;
                    listBrackets.Add(new Brackets(false, i, iRightNum));

                    if (iRightNum > iLeftNum)
                    {
                        bIsErrFormat = true;
                        break;
                    }
                }
            }
            //简单验证
            if (bIsErrFormat || (iLeftNum != iRightNum))
            {
                return Result;
            }

            string sCondition = "";
            string sUpConditon = "";
            //逐层取出条件 
            for (int i = 1; i <= iRightNum; i++)
            {
                var lstRight = listBrackets.Where(p => p.IindexBracketNum == i && p.bIsLeft == false).ToList();
                int ritindex = lstRight.FirstOrDefault().iIndexOfStr;

                var lstLeft = listBrackets.Where(p => p.bIsLeft == true && p.iIndexOfStr < ritindex).OrderByDescending(p => p.iIndexOfStr).ToList();
                int lftindex = lstLeft.FirstOrDefault().iIndexOfStr;

                listBrackets.Remove(lstLeft.FirstOrDefault());

                sCondition = sInput.Substring(lftindex, (lstRight.FirstOrDefault().iIndexOfStr - lftindex + 1));
                if (lftindex < 4)
                {
                    sUpConditon = sInput.Substring(0, lftindex + 1);
                }
                else
                {
                    sUpConditon = sInput.Substring(lftindex - 4, 5);
                }

                Result.Add(new BracketContent(i, sUpConditon, sCondition));
            }

            return Result;

        }

    }

}
