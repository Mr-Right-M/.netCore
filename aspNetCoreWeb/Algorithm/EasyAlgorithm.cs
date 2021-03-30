using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aspNetCoreWeb.algorithm
{
    /// <summary>
    /// 算法： *代表难度 *越多难度越大
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public class EasyAlgorithm : ControllerBase
    {
        [HttpGet]
        public string AlgorithmData(string input1, string input2, string input3)
        {
            var ret = string.Empty;
            //ret = LengthOfLongestSubstring(input).ToString();
            //ret += LengthOfLongestSubstring_fast(input).ToString();
            //var nums1 = new[] { 1, 3 };
            //var nums2 = new[] { 2 };
            //ret = FindMedianSortedArrays(nums1, nums2).ToString();
            //ret = Reverse(100).ToString();

            //ret = RepleaceStr(input1, input2, input3);
            int[][] matirxD = new int[][]
            {
                new int [] { 1 ,3,5}
            };
            ret = SearchMatrix(matirxD, 3).ToString();
            ret = SearchMatrix(matirxD, 0).ToString();
            return ret;
        }


        #region 1. 两数之和（*）
        /// <summary>
        /// 1. 两数之和（*）
        /// 给定一个整数数组 nums 和一个整数目标值 target，请你在该数组中找出 和为目标值 的那 两个 整数，并返回它们的数组下标。
        /// 你可以假设每种输入只会对应一个答案。但是，数组中同一个元素不能使用两遍。
        /// 输入：nums = [2,7,11,15], target = 9
        /// 输出：[0,1]
        /// 解释：因为 nums[0] + nums[1] == 9 ，返回[0, 1]
        /// var tempString = TwoSum(new[] { 3, 2, 4 }, 6);
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public int[] TwoSum(int[] nums, int target)
        {
            var retArray = new int[2];
            for (var i = 0; i < nums.Length; i++)
            {
                for (var j = i + 1; j < nums.Length; j++)
                {
                    if (nums[i] + nums[j] == target)
                    {
                        retArray[0] = i;
                        retArray[1] = j;
                        break;
                    }
                }
            }


            int restNum = 0;
            bool flag = false;
            int inti = 0;
            int intj = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                if (!flag)
                {
                    restNum = target - nums[i];
                    nums[i] = 0;
                    if (nums.Contains(restNum))
                    {
                        flag = true;
                        inti = i;
                        continue;
                        //retArray[0] = (i);
                    }
                }
                if (nums[i] == restNum)
                {
                    intj = i;
                    break;
                    //retArray[1] = i;
                }
            }

            return new[] { inti, intj };
        }
        #endregion

        #region 2、最大无重复字符串（**）
        /// <summary>
        /// 描述：基础版
        /// 姓名：mipan
        /// 日期：2021年3月21日
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int LengthOfLongestSubstring(string s)
        {
            var charArray = s.ToCharArray();
            var lengthOfubstring = new HashSet<char>();
            var j = 0;
            var maxLength = 0;
            for (int i = 0; i < charArray.Length; i++)
            {
                lengthOfubstring.Clear();
                lengthOfubstring.Add(charArray[i]);
                j = i + 1;
                while (j < charArray.Length && !lengthOfubstring.Contains(charArray[j]))
                {
                    lengthOfubstring.Add(charArray[j]);
                    j++;
                }
                maxLength = lengthOfubstring.Count > maxLength ? lengthOfubstring.Count : maxLength;
            }

            return maxLength;
        }

        /// <summary>
        /// 描述：优化版-类似双指针
        /// 姓名：mipan
        /// 日期：2021年3月21日
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int LengthOfLongestSubstring_fast(string s)
        {
            var charArray = s.ToCharArray();
            var j = 0;
            var lengthOfubstring = new HashSet<char>();
            var maxLength = 0;
            for (int i = 0; i < s.Length; i++)
            {
                while (lengthOfubstring.Contains(s[i]))
                {
                    lengthOfubstring.Remove(s[j++]);
                }
                lengthOfubstring.Add(s[i]);
                if (lengthOfubstring.Count > maxLength)
                    maxLength = lengthOfubstring.Count;
            }

            return maxLength;
        }
        #endregion

        #region 3、寻找两个正序数组的中位数
        /// <summary>
        /// 描述：给定两个大小分别为 m 和 n 的正序（从小到大）数组 nums1 和 nums2。请你找出并返回这两个正序数组的 中位数 。
        /// 中位数：如果数据的个数是奇数，则中间那个数据就是这群数据的中位数；如果数据的个数是偶数，则中间那2个数据的算术平均值就是这群数据的中位数
        /// 输入：nums1 = [1,3], nums2 = [2]
        /// 输出：2.00000
        /// 解释：合并数组 = [1,2,3] ，中位数 2
        /// 姓名：mipan
        /// 日期：2021年3月22日
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="nums2"></param>
        /// <returns></returns>
        private double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            double retNumber = 0f;

            var mergeNum = new int[nums1.Length + nums2.Length];
            var num = new int[2];
            if (mergeNum.Length % 2 == 0)
            {
                num[0] = (int)Math.Floor((decimal)mergeNum.Length / 2);
                num[1] = (int)Math.Ceiling((decimal)mergeNum.Length / 2 + 0.01m);
            }
            else
            {
                num[0] = (int)Math.Floor((decimal)mergeNum.Length / 2);
                num[1] = -1;
            }
            for (int i = 0; i < nums1.Length; i++)
            {
                mergeNum[i] = nums1[i];
            }
            for (int i = 0; i < nums2.Length; i++)
            {
                mergeNum[nums1.Length + i] = nums2[i];
            }

            var nowNum = 1;
            var tempSet = new Dictionary<int, int>();

            var tempNum1 = 0;
            var tempNum2 = 0;

            for (int i = 0; i < mergeNum.Length; i++)
            {
                if ((i + 1) < mergeNum.Length && mergeNum[i + 1] > mergeNum[i])
                {
                    nowNum += 1;
                    if (nowNum == num[0] || nowNum == num[1])
                    {
                        tempNum1 = mergeNum[i];
                        if (tempSet.TryGetValue(nowNum, out tempNum2))
                        {
                            tempSet.Add(nowNum, tempNum1 > tempNum2 ? tempNum2 : tempNum1);
                        }
                        else
                        {
                            tempSet.Add(nowNum, tempNum1);
                        }
                    }
                }
                else
                {
                    nowNum -= 1;
                    if ((i + 1) < (mergeNum.Length) && (nowNum == num[0] || nowNum == num[1]))
                    {
                        tempNum1 = mergeNum[i];
                        if (tempSet.TryGetValue(nowNum, out tempNum2))
                        {
                            tempSet.Remove(nowNum);
                            tempSet.Add(nowNum, tempNum1 > tempNum2 ? tempNum1 : tempNum2);
                        }
                        else
                        {
                            tempSet.Add(nowNum, tempNum1);
                        }
                    }
                }

            }

            return (double)tempSet.Values.Sum() / tempSet.Values.Count();
        }
        #endregion

        #region 4、整数反转
        /// <summary>
        /// 描述：给你一个 32 位的有符号整数 x ，返回将 x 中的数字部分反转后的结果。
        /// 如果反转后整数超过 32 位的有符号整数的范围[−231, 231 − 1] ，就返回 0。
        /// 假设环境不允许存储 64 位整数（有符号或无符号）。
        /// 输入：x = 123
        /// 输出：321
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public int Reverse(int x)
        {
            int retNum = 0;
            int temp = 0;

            while (x != 0)
            {
                temp = x % 10;
                //判断是否 大于 最大32位整数
                if (retNum > 214748364 || (retNum == 214748364 && temp > 7))
                {
                    return 0;
                }
                //判断是否 小于 最小32位整数
                if (retNum < -214748364 || (retNum == -214748364 && temp < -8))
                {
                    return 0;
                }
                retNum += retNum * 10 + temp;

                x = x / 10;
            }
            return retNum;
        }
        #endregion

        #region 5、字符串替换（亦我信息技术面试题）

        /// <summary>
        /// A、B、C是3个字符串。把A中包含的所有B都替换为C.如果替换以后还有B就继续替换，直到A不包含B为止。
        /// 1.请编写程序实现以上功能。不允许使用系统提供的字符串比较、查找和替换函数。
        /// 2.以上程序是否总是能正常输出结果?如果不是，列出哪些情况下无法正常输出结果，尽可能详细和全面。
        /// </summary>
        string ret = string.Empty;
        public string RepleaceStr(string a, string b, string c)
        {
            StringBuilder str = new StringBuilder();
            int flag = 0;
            for (var i = 0; i < a.Length; i++)
            {
                for (var j = 0; j < b.Length; j++)
                {
                    if (a[i + j] != b[j] || a.Length - i < (b.Length - j))
                        break;
                    flag++;
                }
                if (flag == b.Length)
                {
                    i += b.Length - 1;
                    str.Append(c);
                }
                else
                {
                    str.Append(a[i]);
                }
            }
            if (ret.Length <= 0 || (ret.Length > str.Length && ret.Length > 0))
            {
                ret = str.ToString();
                return RepleaceStr(str.ToString(), b, c);
            }
            else
                return ret;
        }

        #endregion

        #region
        /// <summary>
        /// 描述：编写一个高效的算法来判断 m x n 矩阵中，是否存在一个目标值。该矩阵具有如下特性：
        /// 每行中的整数从左到右按升序排列。
        /// 每行的第一个整数大于前一行的最后一个整数。
        /// 链接：https://leetcode-cn.com/problems/search-a-2d-matrix
        /// 输入：matrix = [[1,3,5,7],[10,11,16,20],[23,30,34,60]], target = 3
        /// int[][] matirxD = new int[][]
        ///   {
        ///     new int[4]  { 1, 3, 5, 7 } ,
        ///     new int[4] { 10, 11, 16, 20 },
        ///     new int[4] { 23, 30, 34, 60 }
        ///   };
        /// 输出：true
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool SearchMatrix(int[][] matrix, int target)
        {
            bool ret = false;

            // 方法一：行只循环一次、列全循环
            for (var i = 0; i < matrix.Length; i++)
            {
                if (matrix[i][matrix[i].Length - 1] >= target && matrix[i][0] <= target)
                {
                    for (var j = 0; j < matrix[i].Length; j++)
                    {
                        if (matrix[i][j] == target)
                            ret = true;
                    }
                }
            }

            return ret;
        }
        #endregion
    }
}
