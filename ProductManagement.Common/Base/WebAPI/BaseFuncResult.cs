﻿namespace ProductManagement.Common.Base.WebAPI
{
	public class BaseFuncResult<ResultType> : BaseActionResult
	{
		public ResultType ListData { get; set; }
		public BaseFuncResult(): base()
		{
		}
	}
}
