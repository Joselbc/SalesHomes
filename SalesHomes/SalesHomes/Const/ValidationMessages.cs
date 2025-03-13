using SalesHomes.Models;

namespace SalesHomes.Const
{
    public static class ValidationMessages
    {
       public const string REQUIRED_REGISTRATION = "Required for registration";
       public const string CREATED_SALE = "Sale successfully created";
       public const string CLIENT_NOT_FOUND = "Client does not exist.";
       public const string HOUSING_NOT_AVAILABLE = "Housing is not available";
       public const string AGENCY_NOT_FOUND = "Agency does not exist.";
       public const string UNCREATED_SALE = "Sale could not be created due to missing information.";
       public const string SALE_CREATION_ERROR = "An error occurred while creating the sale.";
       public const string SALE_NOT_FOUND = "Sale not found.";
       public const string SALE_DELETED = "Sale deleted successfully.";
       public const string SALE_DELETION_ERROR = "Error occurred while deleting the sale.";

       public const string CREATED_AGENCY = "Agency successfully created";
       public const string AGENCY_CREATION_ERROR = "An error occurred while creating the agency.";

       public const string CREATED_HOUSING = "Housing successfully created";
       public const string RESERVATION_SUCCESSFUL = "The house was successfully reserved";
       public const string SUCCESSFUL_UPGRADE = "The update was successfully executed.";
       public const string HOUSING_CREATION_ERROR = "An error occurred while creating the housing.";
       public const string INVALID_HOUSING_STATUS = "The provided housing status is not valid. Valid statuses are: Available, Sold.";

    }
}


