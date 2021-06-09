create database ecmsdb;



use ecmsdb;

create table e_user(
id int identity(1,1) primary key,
f_name varchar(20) not null,
l_name varchar(20) not null,
dob date not null,
nic varchar(13) unique not null,
tp varchar(10) not null,
addr varchar(50),
user_type varchar(20) not null,
gender varchar(6) not null,
passwd varchar(50) not null,

);

create table subject(
id int identity(1,1) primary key,
name varchar(20) not null
);

create table teacher(
id int identity(1,1) primary key,
name varchar(50) not null,
tp varchar(10) not null
);

create table class(
id int identity(1,1) primary key,
name varchar(50) not  null,
day_ varchar(12) not null,
start_time time not null,
end_time time not null,
free int not null,
teacher_id int foreign key references teacher(id),
subject_id int foreign key references subject(id)
)

create table student(
id int identity(1,1) primary key,
f_name varchar(20) not null,
l_name varchar(20) not null,
dob date not null,
nic varchar(13) unique not null,
tp varchar(10) not null,
addr varchar(50),
gender varchar(6) not null,
regi_date date default(getdate()),
barcode_path varchar(150)
);


create table payment(
id int identity(1,1) primary key,
class_id int foreign key references class(id),
student_id int foreign key references student(id),
date date default(getdate())

);



create table stu_class(
student_id int foreign key references student(id),
class_id int foreign key references class(id),
date date default(getdate()),
 c_no varchar(3)
);





create table attendance(
id int identity(1,1) primary key,
class_id int foreign key references class(id),
student_id int foreign key references student(id),
date date default(getdate()),
time time default(getdate())

);


-----admin----------------------------------------------nic-------------------------------------------------password-
insert into e_user values('admin','admin','2000-1-15','200001502078','0769438974','matale','Manager','Male','12345678')


