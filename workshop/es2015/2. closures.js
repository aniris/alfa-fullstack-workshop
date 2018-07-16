// области видимости
var result = 3;
function addTwo(x) {
  var result = x + 2;
  return result;
}
var x = addTwo(1);
result = result + 1;
console.log("пример области видимости result:", result);

// контекст выполнения
let a = 3;
function addTwo(x) {
  let result = x + 2;
  return result;
}
let b = addTwo(a);
// console.log("пример по контекстам выполнения:", b);

// области видимости
let factor = 2;
function multiplyThis(n) {
  let result = n * factor;
  return result;
}
let multiplied = multiplyThis(6);
//console.log("пример по областям видимости:", multiplied);

// фукция в функции ES5
var firstValue = 7;
function createAdder() {
  function addNumbers(a, b) {
    var result = a + b;
    return result;
  }
  return addNumbers;
}
var adder = createAdder();
var sum = adder(firstValue, 8);
console.log("пример функциональной матрешки: ", sum);

// замыкание
function createCounter() {
  var counter = 0;
  var myFunction = function() {
    counter = counter + 1;
    return counter;
  };
  return myFunction;
}
var increment = createCounter();
var c1 = increment();
var c2 = increment();
var c3 = increment();
console.log("Пример инкремента:", c1, c2, c3);

// фиксация функции ES5
var c = 4;
function addX(x) {
  return function(n) {
    return n + x;
  };
}
var addThree = addX(3);
var d = addThree(c);
console.log("пример фиксации аргумента:", d);