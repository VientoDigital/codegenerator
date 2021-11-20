Code Generator
==============

Code Generator is a database centric template based code generator for any text(ascii) programming language like C, PHP, C#, Visual Basic, Java, Perl, Python… Supported databases are SQL Server, MySQL, PostgreSQL and Oracle.

## Documentation
### Database

- **{DATABASE.NAME}**
Placeholder for the database name.
- **{DATABASE.TABLES}…{/DATABASE.TABLES}**
Placeholder for the tables. Only used for iterating tables. Attempting to further iterate by columns will not work as you may expect.

### Table

- **{TABLE.SCHEMA}**
Placeholder for the table schema.
- **{TABLE.NAME}**
Placeholder for the table name.
- **{TABLE.COLUMNS}…{/TABLE.COLUMNS}**
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

- **{IF COLUMN.NAME =~ ‘Id’}…{/IF}**
Condition to test if the name of the column contains a string.
- **{IF COLUMN.TYPE EQ ‘int’}…{/IF}**
Condition to test if the type of the column is the specified SQL type.
- **{IF NOT COLUMN.NULLABLE}…{/IF}**
Condition to test if a column is nullable or not.
- **{IF NOT LAST}…{/IF}**
Condition to test if it is the last column being processed.

### Custom Values

- **{NAME_OF_YOUR_TAG}**
Custom Values are Key/Value Pairs that you can define to use in a template.

### Options
Some of the expressions allow for certain options to modify the output.
- **{COLUMN.NAME}**, **{DATABASE.NAME}** and custom values let you specify a case conversion. You can choose from any of the following: CAMEL, PASCAL, HUMAN, UNDERSCORE, UPPER, LOWER, HYPHEN, HYPHEN_LOWER, HYPHEN_UPPER. Example:
    {COLUMN.NAME PASCAL}

- **{TABLE.NAME}** lets you specify multiple options. Here are some examples:

`{TABLE.NAME}`: Simply outputs the name, as it appears in the database
`{TABLE.NAME OPTIONS CASE=PASCAL}`: Converts the name to Pascal case
`{TABLE.NAME OPTIONS CASE=UPPER}`: Converts the name to Upper case
`{TABLE.NAME OPTIONS REPLACE(OldValue,NewValue)}`: An expression that allows you to replace a part of the table name with something. This can be useful if your table names tend to have a prefix. For example: `MyCompany_Sales`. To remove the prefix, use `{TABLE.NAME REPLACE(MyCompany_.,)}`
`{TABLE.NAME OPTIONS SINGULARIZE}`: Will ensure the table's name is singularized. Likewise, using `PLURALIZE` instead will pluralize the name.

Options can be combined, but must remain in the same order.. CASE, then REPLACE, then SINGULARIZE or PLURALIZE. Here's an example using all 3 options:
`{TABLE.NAME OPTIONS CASE=PASCAL REPLACE(MyCompany_,) SINGULARIZE}`

### Examples

```
using {Namespace}.Data.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace {Namespace}.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
{DATABASE.TABLES}
        public DbSet<{TABLE.NAME OPTIONS CASE=PASCAL}> {TABLE.NAME OPTIONS CASE=PASCAL} { get; set; }
{/DATABASE.TABLES}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
{DATABASE.TABLES}
            builder.ApplyConfiguration(new {TABLE.NAME OPTIONS CASE=PASCAL}Map());{/DATABASE.TABLES}
        }
    }
}
```

```
using System;
using Extenso.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace {Namespace}.Data.Domain
{
    public class {TABLE.NAME OPTIONS CASE=PASCAL REPLACE(ABC_,) SINGULARIZE} : IEntity
    {{TABLE.COLUMNS}
        public {MAP COLUMN.TYPE} {COLUMN.NAME} { get; set; }
{/TABLE.COLUMNS}
        #region IEntity Members

        public object[] KeyValues
        {
            get { return new object[] { {TABLE.COLUMNS PRIMARY}{COLUMN.NAME}{/TABLE.COLUMNS} }; }
        }

        #endregion IEntity Members
    }

    public class {TABLE.NAME OPTIONS CASE=PASCAL}Map : IEntityTypeConfiguration<{TABLE.NAME OPTIONS CASE=PASCAL}>
    {
        public void Configure(EntityTypeBuilder<{TABLE.NAME OPTIONS CASE=PASCAL}> builder)
        {
            builder.ToTable("{TABLE.NAME}");
           {TABLE.COLUMNS PRIMARY} builder.HasKey(m => m.{COLUMN.NAME});{/TABLE.COLUMNS}{TABLE.COLUMNS NOPRIMARY}
            builder.Property(m => m.{COLUMN.NAME}).HasColumnType("{COLUMN.TYPE}"){IF NOT COLUMN.NULLABLE}.IsRequired(){/IF}.HasMaxLength({COLUMN.LENGTH}).IsUnicode(true);{/TABLE.COLUMNS}
        }
    }
}
```

### Additional Notes:
- If you name your templates using the `{TABLE.NAME…}` expression, it will automatically generate the correct file name for you as well. Examples:
```
{TABLE.NAME OPTIONS CASE=PASCAL}.cs
{TABLE.NAME OPTIONS CASE=PASCAL}Controller.cs
```

### Future Work:

It would be good to have the following work done in future:

- Separate the views from the tables.. example: {VIEW.COLUMNS…}, {VIEW.NAME…}, etc.
- Support for foreign key info, so that we can generate things like EF Navigation Properties