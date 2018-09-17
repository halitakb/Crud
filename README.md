# CrudEntityCSharp

# Nuget Crud.Entity

# Db config
 public class XDataDb : CrudDB
 
    {
    
        public XDataDb()
        
            :base(
            
                 connectionstring:"...",
                 
                 dbCommand:typeof(DbCommand),     //NpgsqlCommand,MySqlDbCommand  ... IDbCommand
                 
                 dbConnection:typeof(DBConnection), //NpgsqlConnection,MySqlConnection ...  IDbConnection
                 
                 DbType:Util.DatabaseName.Postgresql  //  Postgresql,Mssql,Mysql,Oracle (Not test only Postgresql)
                 
                 ){}
                 
        public TModel TModel => new TModel();
        
        public TModel1 TModel1 => new TModel1();
        
        public TModel2 TModel2 => new TModel2();
        
        public TModel3 TModel3=> new TModel3();
        
        public TModel4 TModel4 => new TModel4();
        
        ...
        
    }

# Model

 public class TModel :CrudModel
 
   {
    ....
   }
   
# Facade

 public class TModel : ICrudFacade
 
   {
    ....
   }
   
# AddParam

  Where query parameter
  


# Use

XDataDb db= new XDataDb();

db.TModel

    .Join<TModel, TModel2>(true)  in both cases true or false TModel inner join ?TModel2:TModel on TModel2.TModelID=TModel.id 
    
    .Join<TModel1, TModel2>()
    
    .Join<TModel2, TModel3>()
    
    ...
    
    .AddParam("TModel2.TModelID", TModel.Id,true) //add param last parameter of (true) for start 
    
    .AddParam("TModel2.TModel1ID", TModel1.Id)
    
    .AddSelect("field")
    
    .AddSelect("TModel1.field1","newName")  //field1 as newName
    
    .AddSelect("TModel1.field2","newName2")  //field1 as newName
    
    ..
    
    .ListFacade<TModelFacade>(typeof(TModel).Name);
    

 db.TModel
 
    .Add(TModel2); 
 
 db.TModel
 
    .InsertParam("name", TModel.name)
 
    .InsertParam("sur", TModel.sur)
    
    .Add()
    

 db.TModel
 
    .Edit(TModel2); 
 
 db.TModel
 
     .AddParam("id", TModel.Id,true)
 
    .UpdateParam("name", TModel.name)
    
    .UpdateParam("sur", TModel.sur)
    
    .Update()
    
 db.TModel
 
    .AddParam("name", TModel.name,true)
 
    .AddParam("sur", TModel.sur)
    
    .Remove();
   
   
  # Transaction  #only 3.0.0.2
  
 
  
  db.TModel
  
    .Add(TModel,true)
    
    .Add(TModel2,true)
    
    .EndTrans();
    
  db.TModel
  
    .Add(TModels,true) // TModels-> List<TModel>
    
    .Add(TModel2,true)
    
    .EndTrans();
    
Test purpose and development
