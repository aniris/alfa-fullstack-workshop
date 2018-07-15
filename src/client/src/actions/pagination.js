import * as action from "./types";
import { fetchTransactions } from "./transactions";

export const paginationNext = () => (dispatch, getState) => {
  const { activeCardNumber } = getState().cards;
  let { paginationSkip } = getState().pagination;
  const { data } = getState().transactions;

  if (data.length === 0) return;

  paginationSkip += 10;

  dispatch({
    type: action.PAGINATION_NEXT,
    payload: paginationSkip
  });

  dispatch(fetchTransactions(activeCardNumber, paginationSkip));
};

export const paginationPrev = () => (dispatch, getState) => {
  const { activeCardNumber } = getState().cards;
  let { paginationSkip } = getState().pagination;

  paginationSkip -= 10;
  paginationSkip = paginationSkip < 0 ? 0 : paginationSkip;

  dispatch({
    type: action.PAGINATION_PREV,
    payload: paginationSkip
  });

  dispatch(fetchTransactions(activeCardNumber, paginationSkip));
};