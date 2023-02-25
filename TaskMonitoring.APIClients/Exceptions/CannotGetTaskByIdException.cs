namespace TaskMonitoring.APIClients.Exceptions
{
	public class CannotGetTaskByIdException : WebProxyException
	{
		public CannotGetTaskByIdException(Exception ex = null) : base(ex)
		{

		}
	}
}
