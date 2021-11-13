Code Generator
==============

Code Generator is a database centric template based code generator for any text(ascii) programming language like C, PHP, C#, Visual Basic, Java, Perl, Python... Supported databases are SQL Server, MySQL and PostgreSQL.

## Documentation
### Database

- **{DATABASE.NAME}**
Placeholder for the database name.

### Table

- **{TABLE.SCHEMA}**
Placeholder for the table schema.
- **{TABLE.NAME}**
Placeholder for the table name.
- **{TABLE.COLUMNS}...{/TABLE.COLUMNS}**
Placeholder for the columns. Posisble attributes are: PRIMARY, NOPRIMARY or ALL (default) to filter which columns to process.

### Columns

- **{COLUMN.NAME}**
Placeholder for the column's name.
- **{COLUMN.TYPE}**
Placeholder for the column's type.
- **{MAP COLUMN.TYPE}**
Placeholder for the .NET data type mapped from the source database type. Configurable in the Config.js file.
- **{COLUMN.LENGTH}**
Placeholder for the column's length.
- **{COLUMN.DEFAULT}**
Placeholder for the column's default value.

### Conditional Statements

- **{IF COLUMN.NAME =~ ‘Id’}{/IF}**
Condition to test if the name of the column contains a string.
- **{IF COLUMN.TYPE EQ ‘int’}{/IF}**
Condition to test if the type of the column is the specified SQL type.
- **{IF NOT COLUMN.NULLABLE}{/IF}**
Condition to test if a column is nullable or not.
- **{IF NOT LAST},{/IF}**
Condition to test if it is the last column being processed.

### Custom Values

- **{NAME_OF_YOUR_TAG}**
Custom Values are Key/Value Pairs that you can define to use in a template.

### Examples

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
public class {TABLE.NAME PASCAL}Model : IEntity
{
    {TABLE.COLUMNS}
    /// <summary>
    /// {COLUMN.COMMENT}
    /// </summary>
    public {MAP COLUMN.TYPE} {COLUMN.NAME PASCAL} { get; set; }
    {/TABLE.COLUMNS}
}
```