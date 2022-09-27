namespace Hotel_listing.API.Common;

public static class API_Const
{
    internal const string FILTER = "_filter";
    internal const string SORT = "_sort";
    internal const string INCLUDE = "_include";
    internal const string SIZE = "_size";
    internal const string PAGE = "_page";
    internal const string MAX = "_max";
    internal const string QUERY_DESCR =@"Filter query string in the format <em>field(operator)[value]{logical-operator}field(operator)[value]...</em> 
                       <em>operators => ( =, !=, <, <=, >, >=, in), 
                       logical-operator => { and , &&, or, ||}</em>,
                        in case of <em>in</em> operator add the values like [{value1,value2,value3,...}]
                     ";

    internal const string SORT_DESCR = @"Sort query string in the format <em>field1,field2_desc...</em> 
                       <em>if the <b>fieldname</b> is added as query string the records will be sorted on the ascending order, if the <b>fieldname_desc</b> is added as query string the records will be sorted on descending order</em>
                     ";
    internal const string INCLUDE_DESCR = @"Include query string for including relation in the reponse data: <em>Relation1,Realtion2...</em>";
    internal const string SIZE_DESCR = @"Size query string for setting the page size on paginated response data: <em>10 default</em>";
    internal const string PAGE_DESCR = @"Page query string for setting the page number on paginated response data: <em>1 default</em>";
    internal const string MAX_DESCR = @"Max query string for setting the maximum page size on paginated response data: <em>50 default</em>";
    internal const string ID_DESCR = @"Recor Id";
    internal const string BODY_DESCR = @"Object of type to create";
    internal const string PRODUCES_JSON = @"application/json";
    internal const string SWAGGER_OP_DESCR_GETALL = @"<strong>Get all the records from the database based on filters, pagination, sorting, and relations</strong>";
    internal const string SWAGGER_OP_DESCR_GET = @"<strong>Get one record</strong>";
    internal const string SWAGGER_OP_DESCR_CREATE = @"<strong>Insert a record</strong>";
    internal const string SWAGGER_OP_DESCR_DELETE = @"<strong>Delete a record</strong>";
    internal const string SWAGGER_OP_DESCR_UPDATE = @"<Strong>Update a record</strong>";
    internal const string SWAGGER_OP_DESCR_UPDATE_PARTIAL = @"<strong>Update a record partial</strong>";
    internal const string SWAGGER_RES_DESCR_200 = @"If request resolve with a success response";
    internal const string SWAGGER_RES_DESCR_201 = @"If request creates a record with success";
    internal const string SWAGGER_RES_DESCR_204 = @"If record deleted with sucess";
    internal const string SWAGGER_RES_DESCR_400 = @"If object has not been deleted: the response content contains the validation messages";
    internal const string SWAGGER_RES_DESCR_401 = @"Unauthorized access";
    internal const string SWAGGER_RES_DESCR_403 = @"Forbidden access";
    internal const string SWAGGER_RES_DESCR_404 = @"If object not found";
    internal const string SWAGGER_RES_DESCR_409 = @"If object has business validation: the response content contains the business validation messages";
    internal const string SWAGGER_RES_DESCR_500 = @"If internal server errors occur";
    internal const string GET_ALL = @"Get all";
    internal const string GET = @"Get one";
    internal const string CREATE = @"Create";
    internal const string DELETE = @"Delete";
    internal const string UPDATE = @"Update";
    internal const string UPDATE_PARTIAL = @"Update partial";
    internal const string SERVER_ERROR = "Server error";
    internal const string APP_STARTED = "Application starting...";
    internal const string APP_STOPED_WORKING = "Application stopped working";
    internal const string APP_SHUT_DOWN = "Application is shut down";
    internal const string MIGRATION_ERROR = "An error accured during migrations";
    internal const string CORS_POLICY = "CorsPolicy";
}

