Code Generator
==============

Code Generator is a database centric template based code generator for any text(ascii) programming language like C, PHP, C#, Visual Basic, Java, Perl, Python... Supported databases are SQL Server, MySQL and PostgreSQL.

## Syntax
### Database

**{DATABASE.NAME}**
Returns the database name.
### Table

- **{TABLE.NAME}**
Returns the table name.
- **{TABLE.SCHEMA}**
Returns the table schema.
- **{TABLE.COLUMNS}...{/TABLE.COLUMNS}**
Its a placeholder for the Column tags. Which can have the attributes of PRIMARY, NOPRIMARY or ALL (default) to filter which columns to process.
### Columns

- **{COLUMN.TYPE}**
Returns the column type.
- **{COLUMN.DEFAULT}**
Returns the column default value.
- **{COLUMN.NAME}**
Returns the column name.
- **{COLUMN.LENGTH}**
Returns the column length
- **{MAP COLUMN.TYPE}**
Returns the mapping value to the column type defined in the config file: DataTypeMapping.xml.
Conditional Statements

- **{IF NOT COLUMN.NULLABLE}{/IF}**
Condition to test if column is nullable or not.
- **{IF COLUMN.TYPE EQ ‘int’}{/IF}**
Condition to test if column equals a SQL type.
- **{IF COLUMN.NAME =~ ‘Id’}{/IF}**
Condition to test if the name of the column contains a string.
- **{IF NOT LAST},{/IF}**
Condition to test if it is the last column.
### Custom

- **{NAME_OF_YOUR_TAG}**
Custom Values are Key/Value Pairs that you can define to use on a template.

```
public class {TABLE.NAME PASCAL}Mapping : EntityTypeConfiguration<{TABLE.NAME PASCAL}Model>,IRegisterMapping
{
	public {TABLE.NAME PASCAL}Mapping()
	{
		ToTable("{TABLE.NAME}");

		{TABLE.COLUMNS}
		Property(i => i.{COLUMN.NAME PASCAL}).HasColumnName("{COLUMN.NAME}").HasColumnType("{COLUMN.TYPE}"){IF NOT COLUMN.NULLABLE}.IsRequired(){/IF};
		{/TABLE.COLUMNS}
	}
}
```

```
public class {TABLE.NAME PASCAL}Model:Entity
{
	{TABLE.COLUMNS}
	/// <summary>
	/// {COLUMN.COMMENT}
	/// </summary>
	public {MAP COLUMN.TYPE} {COLUMN.NAME PASCAL} {set;get;}
	{/TABLE.COLUMNS}
}
```