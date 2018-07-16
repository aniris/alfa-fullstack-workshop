import * as actions from "../actions/types";

const initialState = {
  stage: "contract",
  data: [],
  transaction: null,
  error: null
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case actions.PAYMENT_STARTED:
      return {
        ...state,
        isLoading: state.data.length === 0 ? true : false
      };

    case actions.PAYMENT_SUCCESS:
      return {
        ...state,
        data: payload,
        error: null,
        isLoading: false
      };

    case actions.PAYMENT_FAILED:
      return {
        ...state,
        error: payload,
        isLoading: false
      };

    default:
      return state;
  }
};
