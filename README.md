# CrudEntityCSharp

# Nuget Crud.Entity

public class DbClass:CrudEntity

   public DbClass()

   {

      newDbCommand();

   }

   public void newDbCommand()

   {

       string conString = @"....";

       setConnection<XDbCommand,XSQlConnection>(conString);

   }

   public CrudEntity<XModel> XModel => new CrudEntity<XModel>();


#Add

    ModelTable modelTable = new ModelTable()
          {
          .. = ...,
          .. = ...,
          };
      db.newDbCommand();
      db.ModelTable.Add(modelTable);

# Edit

     string[] fields = { "...1Field..", "..2Field..." };

     object[] values = { Value1, value2 };

     db.newDbCommand();

     db.ModelTable.Edit(fields, values);

# Edit<T>

    db.newDbCommand();

    db.xtable.Edit(xtable);

# _Edit(string[] fields, object[] values)

    Not use Addwithvalue..

# Remove

    db.newDbCommand();

    db.xtable.Remove(Id); //int

# where

    limit 100

#Select

 List<SQLComparison> qLComparison = new List<SQLComparison>()
    {  

    new SQLComparison("x.XXXID", XXValue, SQLComparison.Operators.EqualTo, SQLComparison.Bitwise.AND),  

    --where x.XXXID=xxValue and x.xxxname=xxnamevalue

       new SQLComparison("x.xxxname",xxnamevalue, SQLComparison.Operators.EqualTo, SQLComparison.Bitwise.NULL)  };

        List<SqlJoinField> JoinFields = new List<SqlJoinField>()

    {   

      new SqlJoinField("Xtable","XtableID"),        // inner join xtable on TModel.Id=xtableID

      new SqlJoinField("Xtable1","xtable1ID","Xtable2","xtable2ID")  //inner join xtable1 on xtable1.xtable1ID=xtable2=xtable2ID

    };
    
    List<SqlSelectField> sqlFields = new List<SqlSelectField>()   
    
    {
    
       new SqlSelectField("xtable1.Name","Name"),   // select xtable1.Name as Name
    
    };
    
    return dbEntity.TModel.Select<XtableFc>(sqlFields, qLComparison, JoinFields, true, CrudStatic.OrderByParam.OrderByAsc, "Id");


