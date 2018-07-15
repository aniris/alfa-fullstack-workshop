import * as actions from "../actions/types";

const initialState = {
  paginationSkip: 0
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case actions.PAGINATION_NEXT:
      return {
        ...state,
        paginationSkip: payload
      };

    case actions.PAGINATION_PREV:
      return {
        ...state,
        paginationSkip: payload
      };

    case actions.PAGINATION_RESET:
      return {
        ...state,
        paginationSkip: 0
      };

    default:
      return state;
  }
}