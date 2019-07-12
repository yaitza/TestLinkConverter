namespace ConvertModel
{
    public enum ImportanceType
    {
        /// <summary>
        /// 测试用例针对默认未设置情况
        /// </summary>
        未设置= 0,
        /// <summary>
        /// 测试用例重要性为低
        /// </summary>
        低 = 1,

        /// <summary>
        /// 测试用例重要性为中
        /// </summary>
        中 = 2,

        /// <summary>
        /// 测试用例重要性为高
        /// </summary>
        高 = 3,
    }
}