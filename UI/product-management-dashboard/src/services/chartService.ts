const possibleColours = [
    '#f0f8ff',
    '#faebd7',
    '#00ffff',
    '#7fffd4',
    '#f0ffff',
    '#f5f5dc',
    '#ffe4c4',
    '#000000',
    '#ffebcd',
    '#0000ff',
    '#8a2be2',
    '#a52a2a',
    '#deb887',
];

export function generateColorScheme(numberOfColoursToGenerate: number): string[] {
    const colours: string[] = [];

    for (let i = 0; i < numberOfColoursToGenerate; i++) {
        colours.push(possibleColours[i]);
    }

    return colours;
}