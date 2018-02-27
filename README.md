# CrudEntityCSharp

# Nuget Crud.Entity
Definition of Dbcommand,

Definition of DbConnection,

Definition of Class For Model(Table)

#create context class.. 

public class CrudClass 
{

  public CrudClass(){
   
   CrudStatic.setConnection(connectionstring,xDbCommand,xDbConnection);

  }

private CrudEntity<XClass> _tablename1;

        public CrudEntity<XClass> Tablename1
        {
            get { return _tablename1; }
            set { _tablename1 = new CrudEntity<XClass>("Table1"); }
        }
        public CrudEntity<XClass1> Tablename2
        {
            get { return _tablename2; }
            set { _tablename2 = new CrudEntity<XClass>("Table2"); }
        }


// or
      	private CrudEntity<XClass> Table1 get{};set{};
	      private CrudEntity<XClass1> Table2 get{};set{};

# "Table1" is must for Database in table name..

default tablename -> "Table1"

# call ->>

CrudClass.Table1.Add(new XClass(){ ... ..});
CrudClass.Table2.Add(new XClass1(){ ... ..});


Add ,Remove,Update,
return List Model 

...


connectionString="......"; // Test LocalDb

we have methods..,


Test purpose and development


