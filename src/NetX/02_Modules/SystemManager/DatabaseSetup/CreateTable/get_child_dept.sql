CREATE FUNCTION `get_child_dept`(in_id varchar(50)) RETURNS varchar(1000) CHARSET utf8
begin 
 declare ids varchar(1000) default ''; 
 declare tempids varchar(1000); 
 
 set tempids = in_id; 
 while tempids is not null do 
  set ids = CONCAT_WS(',',ids,tempids); 
  select GROUP_CONCAT(id) into tempids from sys_dept where FIND_IN_SET(parentid,tempids)>0;  
 end while; 
 return ids; 
end