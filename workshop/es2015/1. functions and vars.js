// ===== Function expressions
var testFunc = function() {
  return 1 + 2;
};

function testFunc() {
  return 1 + 1;
}

// console.log(testFunc());

// ===== Vars bubble up
function testFunc() {
  return number + testSum(number);
}

var testSum = function() {
  return number + 2;
};

var number = 1;

// console.log(testFunc());

// ===== Vars areas
function count() {
  for (var i = 0; i < 3; i++) {
    var j = i * 2;
  }

  console.log(i);
  console.log(j);
}

// count();
