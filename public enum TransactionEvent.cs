public enum TransactionEvent
    {
        UpdateOnRowChange = 0,
        UpdateOnSaveButton = 1,
        UpdateOnPrimaryKey = 2,
        UpdateOnFormClose = 3,
        UpdateOnNewButton = 4,
        UpdateOnSaveAndNew = 5,
        UpdateOnInvokeSearch = 6,
        AddNewOnNewButton = 7,
        AddNewOnNewFromGrid = 8,
        AddNewOnPrimaryKey = 9,
        DeleteOnDeleteButton = 10,
        DeleteOnDeleteAddNew = 11,
        DeleteAttachment = 12,
        UndoOnUndoButton = 13,
        UndoOnUndoAddNew = 14,
        UndoOnClearButton = 15,
        UndoOnRefreshButton = 16,
        None = 17,
        FormClosed = 18,
        TransactionCallback = 19,
        PrintDialog = 20
    }