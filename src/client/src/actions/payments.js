import * as action from "./types";
import axios from "axios";
import { fetchTransactions } from "./transactions";

const ROOT_URL = "/api";

/**
 * Проводит withdraw транзакцию
 *
 * @param {String} from
 * @param {String} to
 * @param {Integer} sum
 * @returns
 */
export const TransferMoney = (from, to, sum) => {
  return async dispatch => {
    try {
      dispatch({
        type: action.PAYMENT_STARTED
      });

      const response = await axios.post(`${ROOT_URL}/transactions`, { sum, from, to });

      dispatch({
        type: action.PAYMENT_SUCCESS,
        payload: response.data
      });

      dispatch(fetchTransactions(response.data.from))
    }
    catch (err) {
      console.log(err);

      dispatch({
        type: action.PAYMENT_FAILED,
        payload: err.response
      });
      console.log(
        err.response
      );
    }
  }
};

export const repeateTransferMoney = () => dispatch =>
  dispatch({
    type: action.PAYMENT_REPEAT
  });
