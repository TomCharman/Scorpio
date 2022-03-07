var DateTime = luxon.DateTime;

export const getRelativeTime = (utcString) => {
    return DateTime
        .fromISO(utcString, { zone: 'utc' })
        .toRelative()
}