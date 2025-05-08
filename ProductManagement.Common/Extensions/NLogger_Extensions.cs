namespace ProductManagement.Common.Extensions
{
	public static class NLogger_Extensions
	{
		public static void LogException(this NLog.ILogger logger, Exception ex, string methodName, string param, string route = "")
		{
			string fullMessage = "\tLog Error - " + methodName;
			if (!string.IsNullOrEmpty(route))
				fullMessage += Environment.NewLine + "\troute : " + route;
			fullMessage += Environment.NewLine + "\tparam : " + param;
			fullMessage += Environment.NewLine + ex.AsLogText();

			logger.Error(fullMessage);
		}

		public static void LogInfo(this NLog.ILogger logger, string methodName, string param, int? statusCode = 0, string? message = "", string route = "")
		{
			string fullMessage = "\tLog Info - " + methodName;
			if (!string.IsNullOrEmpty(route))
				fullMessage += Environment.NewLine + "\troute : " + route;
			fullMessage += Environment.NewLine + "\tparam : " + param;
			if (statusCode != 0)
				fullMessage += Environment.NewLine + "\tstatus code : " + statusCode.ToString();
			if (!string.IsNullOrEmpty(message))
				fullMessage += Environment.NewLine + "\tmessage : " + message;

			logger.Info(fullMessage);
		}
	}
}
