import { combineReducers } from "redux";

import cards from "./cards_reducer";
import transactions from "./transactions_reducer";
import payment from "./payment_reducer";
import pagination from "./pagination_reducer";

export default combineReducers({
  cards,
  transactions,
  payment,
  pagination
});
