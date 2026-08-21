-- SQL setup scripts go here

CREATE TABLE clients ( 
    client_id INTEGER AUTO_INCREMENT, 
    first_name VARCHAR(255) NOT NULL, 
    last_name VARCHAR(255) NOT NULL,  
    email VARCHAR(255) UNIQUE, 
    is_archived BOOLEAN DEFAULT FALSE, 
    PRIMARY KEY (client_id) 
);
-- Look up table for phone number types
CREATE TABLE phone_number_types(
    phone_number_type_id INTEGER AUTO_INCREMENT,
    type_name VARCHAR(100),
    display_name VARCHAR(100),
    sort_order int,
    PRIMARY KEY(phone_number_type_id)
);
INSERT INTO phone_number_types (type_name,display_name,sort_order) VALUES
('mobile','mobile',1),
('home','home',2),
('work','work',3);

CREATE TABLE phone_numbers(
    phone_number_id INTEGER AUTO_INCREMENT, 
    client_id INTEGER,
    country_code INTEGER NOT NULL,
    phone VARCHAR(15) NOT NULL,
    is_primary BOOLEAN DEFAULT TRUE,
    number_type_id INTEGER ,
    PRIMARY KEY(phone_number_id),
    FOREIGN KEY(number_type_id) REFERENCES phone_number_types(phone_number_type_id),
    FOREIGN KEY(client_id) REFERENCES clients(client_id)
);