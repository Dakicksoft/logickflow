﻿using System;

namespace Logickflow.Core.Data.Entity
{
    public class WorkflowBookmarkEntity
    {
        public string BOOKMARK_ID { get; set; }

        public string WORKFLOW_INSTANCE_ID { get; set; }

        public string FORM_TYPE { get; set; }

        public string FORM_ID { get;set; }

        public string USER_ID { get; set; }

        public string CURRENT_ACTIVITY_NAME { get; set; }

        public string NEXT_ACTIVITY_NAME { get; set; }

        public DateTime CREATED_ON { get; set; }

        public DateTime LAST_UPDATED_ON { get; set; }

        public string ALLOWED_OPERATIONS { get; set; }
    }
}