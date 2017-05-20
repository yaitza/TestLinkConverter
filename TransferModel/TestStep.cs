using System;

namespace TransferModel
{
    public class TestStep
    {
        /// <summary>
        /// 测试步骤编号
        /// </summary>
        public int StepNumber { get; set; }

        /// <summary>
        /// 测试步骤动作
        /// </summary>
        public string Actions { get; set; }

        /// <summary>
        /// 期望的结果
        /// </summary>
        public string ExpectedResults { get; set; }

        /// <summary>
        /// 执行方式
        /// </summary>
        public ExecType ExecutionType { get; set; }
    }
}