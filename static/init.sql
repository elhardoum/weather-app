create table if not exists cities (
    id bigint unsigned primary key,
    name varchar(150) not null,
    countrycode varchar(2) not null,
    lat decimal not null,
    lon decimal not null
);

create index index_search on cities(name);