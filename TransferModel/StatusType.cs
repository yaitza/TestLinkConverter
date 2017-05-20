namespace TransferModel
{
    public enum StatusType
    {
        /// <summary>
        /// 用例审批状态为Draft
        /// </summary>
        Draft = 1,

        /// <summary>
        /// 用例审批状态为Ready for review
        /// </summary>
        ReadyForReview = 2,

        /// <summary>
        /// 用例审批状态为Review in progress
        /// </summary>
        ReviewInProgress = 3,

        /// <summary>
        /// 用例审批状态为Rework
        /// </summary>
        Rework = 4,

        /// <summary>
        /// 用例审批状态为Obsolete
        /// </summary>
        Obsolete = 5,

        /// <summary>
        /// 用例审批状态为Future
        /// </summary>
        Future = 6,

        /// <summary>
        /// 用例审批状态为Final
        /// </summary>
        Final = 7,
    }
}