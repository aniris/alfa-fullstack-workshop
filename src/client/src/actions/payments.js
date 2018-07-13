import * as action from "./types";

/**
 * Проводит withdraw транзакцию
 *
 * @param {String} from
 * @param {String} to
 * @param {Integer} sum
 * @returns
 */
export const TransferMoney = (from, to, sum) => {
  //TODO
};

export const repeateTransferMoney = () => dispatch =>
  dispatch({
    type: action.PAYMENT_REPEAT
  });
