export interface ITimePeriodFilter {
    description: string,
    minimumDate: Date,
    timePeriod: TimePeriod

}

export enum TimePeriod {
    AllTime = 1,
    LastYear = 2,
    LastMonth = 3,
    LastWeek = 4
}