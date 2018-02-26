# CrudEntityCSharp

# Nuget Crud.Entity

# First set 
DbCrudConnection.setConnection(connectionString,DbCommand,DbConnection);

CrudEntity dBContext = new CrudEntity<TModel>();
 
 default table name "Table1"
 or
 
 CrudEntity dBContext = new CrudEntity<TModel>(tableName);
 
 
 
 
DbCommand -> SqlCommand..
DbConnection -> SqlConnection ..


we have methods..,

 crud.Add(XModel); //return boolean
 
 crud.Edit(XModel); //return boolean
 
 crud.Remove(Id); //return boolean
 
 crud.getLists(); // return List<XModel>
 

Test purpose and development


