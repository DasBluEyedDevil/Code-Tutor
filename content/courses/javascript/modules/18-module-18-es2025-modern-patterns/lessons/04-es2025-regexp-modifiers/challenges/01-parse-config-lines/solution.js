const configRegex = /(?i:port)=(.+)/;

const match = configRegex.exec('port=MyValue');
console.log(match[1]);