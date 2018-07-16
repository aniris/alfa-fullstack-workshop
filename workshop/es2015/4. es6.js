// фиксация функции ES6
let c = 4;
const addX = x => n => n + x;
const addThree = addX(3);
const d = addThree(c);
console.log(`пример фиксации аргумента:${d}`);

// let
let apples = 5;

if (true) {
  let apples = 10;

  console.log(apples); // 10 (внутри блока)
}

console.log(apples); // 5

// let and for
let f = [];
for (var i = 0; i < 10; i++) {
  f.push(function() {
    console.log(i);
  });
}

f[1]();
f[2]();

let f2 = [];
for (let i = 0; i < 10; i++) {
  f2.push(function() {
    console.log(i);
  });
}

f2[1]();
f2[2]();

// desctruction
let [firstName, lastName] = ["Илья", "Кантор"];

console.log(firstName);
console.log(lastName);

let [first, last, ...rest] = ["Юлий", "Цезарь", "Император", "Рима"];

console.log(first);
console.log(last);
console.log(rest);

// 2
const func = ({ title, width = 2, height }) => {
  console.log(title);
  console.log(width);
  console.log(height);
};

// func(options);

// let options = {
//   title: "Меню",
//   height: 200,
//   name: "Super menu"
// };

// const func2 = ({ title, width = 2, height })
//   => ({ title, width, height });

// console.log(func2(options));

// class

class User {
  constructor(firstName, lastName) {
    this.firstName = firstName;
    this.lastName = lastName;
  }

  hello() {
    return `Hello ${this.firstName} ${this.lastName}`;
  }

  static createGuest() {
    return new User("Гость", "Сайта");
  }
}

let guest = User.createGuest();
console.log(guest.hello());
