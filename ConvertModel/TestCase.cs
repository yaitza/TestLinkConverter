using System.Collections.Generic;

namespace ConvertModel
{
    public class TestCase
    {
        /// <summary>
        /// 内部ID
        /// </summary>
        public string InternalId { get; set; }

        public List<string> TestCaseHierarchy { get; set; }

        /// <summary>
        /// 测试用例名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 不清楚具体含义
        /// </summary>
        public string NodeOrder { get; set; }

        /// <summary>
        /// 测试用例编号
        /// </summary>
        public string ExternalId { get; set; }

        /// <summary>
        /// 测试用例Version，不清楚具体含义
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 测试用例的摘要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 测试用例的前提条件
        /// </summary>
        public string Preconditions { get; set; }

        /// <summary>
        /// 测试用例的测试方式
        /// </summary>
        public ExecType ExecutionType { get; set; }

        /// <summary>
        /// 测试用例重要性
        /// </summary>
        public ImportanceType Importance { get; set; }

        /// <summary>
        /// 预估执行时间
        /// </summary>
        public double EstimatedExecDuration { get; set; }

        /// <summary>
        /// 测试用例审批状态
        /// </summary>
        public StatusType Status { get; set; }

        /// <summary>
        /// 测试用例的关键字
        /// </summary>
        public List<string> Keywords { get; set; }

        /// <summary>
        /// 测试用例关联需求
        /// </summary>
        public string Requirement { get; set; }

        /// <summary>
        /// 测试用例测试步骤
        /// </summary>
        public List<TestStep> TestSteps { get; set; } 
    }
}
