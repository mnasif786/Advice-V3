angular.module('adviceapp')
    .constant(
        'enums', {
           taskTypes: {
                    BusinessWiseRequest: 1,
                    Callback30Minutes: 2,
                    Callback1Hour: 3,
                    CaseReview: 4,
                    Critique: 5,
                    DocumentReview: 6,
                    Email: 7,
                    Fax: 8,
                    FollowupTask: 9,
                    Post: 10,
                    SpecialProjectRequest: 11,
                    Misc: 12,
                    Call: 13,
                    CCEmail: 14,
                    FaxManual: 15,
                    RiskAssReview: 16,
                    RequestforConsultancy: 17,
                    InternalEmail: 18,
                    InternalCCEmail: 19,
                    HronlineAdviceRequest: 20,
                    HronlineClientActionAdded: 21,
                    HronlineCloseJobRequest: 22,
                    HronlineReopenJobRequest: 23
                }
        }
    )
    .constant(
        'constants', {
            slaTime: ['08:00', '08:30', '09:00', '09:30', '10:00', '10:30', '11:00', '11:30', '12:00', '12:30', '13:00', '13:30', '14:00', '14:30', '15:00', '15:30', '16:00', '16:30', '17:00', '17:30', '18:00', '18:30', '19:00', '19:30']
        }
    );
