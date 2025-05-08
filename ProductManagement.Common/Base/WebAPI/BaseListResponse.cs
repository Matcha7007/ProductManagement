namespace ProductManagement.Common.Base.WebAPI
{
	public class BaseListResponse<TDto> : BaseResponse<BaseListResponseData<TDto>> where TDto : BaseDto
	{

		/// <summary>
		/// Constructor.
		/// </summary>
		public BaseListResponse()
			: base()
		{ }
	}
}
