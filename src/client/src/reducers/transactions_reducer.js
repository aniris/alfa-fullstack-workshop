import * as actions from "../actions/types";

const initialState = {
  data: [],
  error: null,
  isLoading: true
};

export default (state = initialState, { type, payload }) => {
  switch (type) {
    case actions.TRANS_FETCH_STARTED:
      return {
        ...state,
        isLoading: state.data.length === 0 ? true : false
      };

    case actions.TRANS_FETCH_SUCCESS:
      return {
        ...state,
        data: payload,
        error: null,
        isLoading: false
      };

    case actions.TRANS_FETCH_FAILED:
      return {
        ...state,
        error: payload,
        isLoading: false
      };

    case actions.CARDS_FETCH_FAILED:
      return initialState;

    default:
      return state;
  }
};
