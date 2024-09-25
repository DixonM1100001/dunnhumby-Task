import { ITimePeriodFilter, TimePeriod } from "../models/interfaces/timePeriodFilter";

function subtractYears(date: Date, years: number): Date {
    const dateCopy = new Date(date);
    dateCopy.setFullYear(date.getFullYear() - years);
    return dateCopy;
}

function subtractMonths(date: Date, months: number): Date {
    const dateCopy = new Date(date);
    dateCopy.setMonth(date.getMonth() - months);
    return dateCopy;
}

function subtractDays(date: Date, days: number): Date {
    const dateCopy = new Date(date);
    dateCopy.setDate(date.getDate() - days);
    return dateCopy;
}

export function getFilterTimeFrames(): ITimePeriodFilter[] {
    const currentDate = new Date();

    const timeFrames = [
        {
          description: 'Added any time',
          // Minimum Date in JS
          minimumDate: new Date(-8640000000000000),  
          timePeriod: TimePeriod.AllTime
        },
        {
            description: 'Added in the last year',
            minimumDate: subtractYears(currentDate, 1),
            timePeriod: TimePeriod.LastYear
        },
        {
            description: 'Added in the last month',
            minimumDate: subtractMonths(currentDate, 1),
            timePeriod: TimePeriod.LastMonth
        },
        {
            description: 'Added in the last week',
            minimumDate: subtractDays(currentDate, 7),
            timePeriod: TimePeriod.LastWeek
        }
    ];

    return timeFrames;
}