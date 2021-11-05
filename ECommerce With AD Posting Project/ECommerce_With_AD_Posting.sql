create database ECommerce_With_AD_Posting

use ECommerce_With_AD_Posting

create table tbl_admin
(
ad_id int primary key identity,
ad_name nvarchar(50) not null,
ad_password nvarchar(50) not null,
ad_email nvarchar(50) unique not null
)

create table tbl_cateogry
(
cat_id int primary key identity,
cat_name nvarchar(50) not null,
cat_image nvarchar(max) not null,
cat_status int,
ad_id_fk int foreign key references tbl_admin(ad_id)
)

create table tbl_user
(
u_id int primary key identity,
u_name nvarchar(50) not null,
u_email nvarchar(50) unique not null,
u_password nvarchar(50) not null,
u_image nvarchar(max) not null,
u_contact nvarchar(50) not null,
ad_id_fk int foreign key references tbl_admin(ad_id),
cat_id_fk int foreign key references tbl_cateogry(cat_id)
)

create table tbl_product
(
pro_id int primary key identity,
pro_name nvarchar(50) not null,
pro_price int,
pro_desc nvarchar(100) not null,
pro_image nvarchar(max) not null,
ad_id_fk int foreign key references tbl_admin(ad_id),
cat_id_fk int foreign key references tbl_cateogry(cat_id),
u_id_fk int foreign key references tbl_user(u_id)
)

create table tbl_order
(
order_id int primary key identity,
order_amount float,
order_quantity int,
order_date datetime,
ad_id_fk int foreign key references tbl_admin(ad_id),
cat_id_fk int foreign key references tbl_cateogry(cat_id),
u_id_fk int foreign key references tbl_user(u_id),
pro_id_fk int foreign key references tbl_product(pro_id)
)

create table tbl_invoice
(
inv_id int primary key identity,
ad_id_fk int foreign key references tbl_admin(ad_id),
cat_id_fk int foreign key references tbl_cateogry(cat_id),
u_id_fk int foreign key references tbl_user(u_id),
pro_id_fk int foreign key references tbl_product(pro_id),
o_id_fk int foreign key references tbl_order(order_id),
total_bill float,
inv_date datetime
)

select * from tbl_cateogry



insert into tbl_admin values ('Usman','pakistan456','usman120@gmail.com')

select * from tbl_admin

select * from tbl_user
