# CrudEntityCSharp

# Nuget Crud.Entity
Definition of Dbcommand,

Definition of DbConnection,

Definition of Class For Model(Table)


 public class XDataDb : CrudEntity<XModel, XxxDbcommand, XxxDbConnection>
    {
    
...

connectionString="......"; // Test LocalDb

CrudEntity crud= new CrudEntity(connectionString);

connectionString="......"; // Test LocalDb

tableName="Table1"; //In Database

CrudEntity crud= new CrudEntity(connectionString,tableName);

we have methods..,

 crud.Add(XModel); //return boolean
 
 crud.Edit(XModel); //return boolean
 
 crud.Remove(Id); //return boolean
 
 crud.getLists(); // return List<XModel>
 

Test purpose and development


