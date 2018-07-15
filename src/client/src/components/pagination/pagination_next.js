import React, { Component } from "react";
import { connect } from "react-redux";
import Button from "../misc/button";
import {paginationNext} from "../../actions/pagination";

class PaginationNext extends Component {
  constructor(props) {
    super(props);

    this.onClickWrapper = this.onClickWrapper.bind(this);
  }

  onClickWrapper() {
    this.props.onClick();
  }

  render() {
    return (
      <div onClick = {this.onClickWrapper}>
        <Button>
          Вперед
        </Button>
      </div>
    )
  }
}

const mapStateToProps = state => ({
  activeCardNumber: state.cards.activeCardNumber,
  paginationNext: state.cards.paginationNext
});

const mapDispatchToProps = dispatch => ({
  onClick: () => dispatch(paginationNext())
});

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(PaginationNext);
