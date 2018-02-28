# CrudEntityCSharp

# Nuget Crud.Entity

#create context class.. 

public class xxCrudClass 
{

  public xxCrudClass()
  	{
   
	CrudStatic.setConnection(connectionstring,new xDbCommand(),new xDbConnection());

	}
	
   	private CrudEntity<Table1> xxTable1 =new CrudEntity<Table1>("Table1");
	private CrudEntity<Table2> xxTable2 =new CrudEntity<Table2>("Table2");
	
..
..

# "Table1" Model 's name is must database in tablename.
# "Table2" Model 's name is must database in tablename.
..
..

default tablename -> your Model Name..

# call ->>

xxCrudClass.Table1.Add(new Table1(){ ... ..});
xxCrudClass.Table2.Add(new Table2(){ ... ..});
List<Table2> xxLists =d.Table2.getJoin<Table1>();

Add ,Remove,Update,getJoin
return List Model 


...


connectionString="......"; // Test LocalDb

we have methods..,


Test purpose and development


