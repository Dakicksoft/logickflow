/*WORKFLOW_TEMPLATES*/
CREATE TABLE MSC_WORKFLOW_TEMPLATES(
       WORKFLOW_TEMPLATE_UUID CHAR(36) NOT NULL PRIMARY KEY,
       WORKFLOW_NAME nvarchar(400) NOT NULL,
       WORKFLOW_TEMPLATE_VERSION numeric(10) DEFAULT (1) NOT NULL,
       WORKFLOW_TEMPLATE_URI varchar(255) NOT NULL,
       CREATED_ON DATE NOT NULL,
       IS_ENABLE numeric(1) DEFAULT(1) NOT NULL
);


CREATE TABLE MSC_ACTION(
       ACTION_ID CHAR(36) NOT NULL PRIMARY KEY,
       ACTIVITY_INSTANCE_ID CHAR(36) NOT NULL,
       OPERATION_CODE varchar(50) NOT NULL,
       REQUIRED_ROLE varchar(50) NOT NULL,
       REQUIRED_OPERATOR_TYPE numeric(10) NOT NULL,
       CREATED_ON DATE NOT NULL,
       LAST_UPDATED_ON DATE NOT NULL,
       APPROVER_REQUIRED varchar(50),
       STATUS numeric(10) NOT NULL
);

CREATE TABLE MSC_WORKFLOW_INSTANCE(
       WORKFLOW_INSTANCE_ID CHAR(36) NOT NULL PRIMARY KEY,
       WORKFLOW_TEMPLATE_ID CHAR(36) NOT NULL,
       FORM_TYPE varchar(50) NOT NULL,
       FORM_ID varchar(50) NOT NULL,
       OWNER_ID CHAR(36) NOT NULL,
       CREATED_ON DATE NOT NULL,
       LAST_UPDATED_ON DATE NOT NULL,
       INSTANCE_VERSION DATETIME2 NOT NULL,
       STATUS numeric(10) NOT NULL
);

CREATE TABLE MSC_ACTIVITY_INSTANCE(
       ACTIVITY_INSTANCE_ID CHAR(36) NOT NULL PRIMARY KEY,
       WORKFLOW_INSTANCE_ID CHAR(36) NOT NULL,
       ACTIVITY_TEMPLATE_ID varchar(50) NOT NULL,
       POLICY_DESCRIPTOR VARCHAR(400),
       CREATED_ON DATE NOT NULL,
       LAST_UPDATED_ON DATE NOT NULL,
       STATUS numeric(10) NOT NULL
);

CREATE TABLE MSC_WORKFLOW_AUDIT(
       AUDIT_ID CHAR(36) NOT NULL PRIMARY KEY,
       ACTION_ID CHAR(36) NOT NULL,
       OPERATOR_ID CHAR(36) NOT NULL,
       IS_DELEGATED numeric(1) NOT NULL,
       OPERATOR_COMMENT varchar(400),
       CREATED_ON DATE NOT NULL
);

CREATE TABLE MSC_WORKFLOW_BOOKMARK(
       BOOKMARK_ID CHAR(36) NOT NULL PRIMARY KEY,
       WORKFLOW_INSTANCE_ID CHAR(36) NOT NULL,
       FORM_TYPE varchar(50) NOT NULL,
       FORM_ID CHAR(36) NOT NULL,
       USER_ID CHAR(36) NOT NULL,
       CURRENT_ACTIVITY_NAME varchar(400) NOT NULL,
       NEXT_ACTIVITY_NAME varchar(400) NOT NULL,
       CREATED_ON DATE NOT NULL,
       LAST_UPDATED_ON DATE NOT NULL,
       ALLOWED_OPERATIONS varchar(400) NOT NULL
);