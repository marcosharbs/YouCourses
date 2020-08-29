
    create table "Author" (
        Id uuid not null,
       Name varchar(255),
       ImageUrl varchar(400),
       primary key (Id)
    )
    create table "Course" (
        Id uuid not null,
       Author_id uuid,
       Name varchar(255),
       Description varchar(400),
       primary key (Id)
    )
    create table "Video" (
        Id uuid not null,
       Name varchar(255),
       Url varchar(400),
       Course_id uuid,
       primary key (Id)
    )
    alter table "Course" 
        add constraint FK_4F0D5EE9 
        foreign key (Author_id) 
        references "Author"
    alter table "Video" 
        add constraint FK_6053BF81 
        foreign key (Course_id) 
        references "Course"