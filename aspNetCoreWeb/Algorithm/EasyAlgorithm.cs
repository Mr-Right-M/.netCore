using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public string AlgorithmData(string input)
        {
            var ret = string.Empty;
            ret = LengthOfLongestSubstring(input).ToString();
            ret += LengthOfLongestSubstring_fast(input).ToString();
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
    }
}
