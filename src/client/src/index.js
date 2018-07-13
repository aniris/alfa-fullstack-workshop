import React from "react";
import ReactDOM from "react-dom";

// Redux
import { Provider } from "react-redux";
import { createStore, applyMiddleware, compose } from "redux";

// Middlewares
import reduxThunk from "redux-thunk";

// Components
import App from "./components/app";

import reducers from "./reducers";

// Middlewares inits
const middlewares = [reduxThunk];

const composeEnhancers = window.__REDUX_DEVTOOLS_EXTENSION_COMPOSE__ || compose;
const store = createStore(
  reducers,
  composeEnhancers(applyMiddleware(...middlewares))
);

ReactDOM.render(
  <Provider store={store}>
    <App />
  </Provider>,
  document.getElementById("root")
);
