package com.spacebrains.widgets;

import com.spacebrains.interfaces.INamed;
import com.spacebrains.util.BaseParams;

import javax.swing.*;
import javax.swing.table.DefaultTableCellRenderer;
import java.awt.*;
import java.util.ArrayList;

import static com.spacebrains.util.BaseParams.BASE_TABLE_HEADER_FONT;

public class BaseTable extends JPanel {

    private final int TABLE_WIDTH = 350;
    private final int TABLE_HEIGHT = 192;

    private JTable table;
    ArrayList<? extends INamed> values;
    private final Button addBtn = new Button("Добавить");
    private final Button editBtn = new Button("Изменить");
    private final Button deleteBtn = new Button("Удалить");
    private GridBagConstraints gbc = new GridBagConstraints();

    public BaseTable(ArrayList<? extends INamed> values) {
        super();
        this.values = values;
        setLayout(new GridBagLayout());

        table = new JTable(new NamedTableModel(values));
        table.setFont(BaseParams.BASE_TABLE_FONT);
        table.setRowHeight(22);

        table.getTableHeader().setFont(BASE_TABLE_HEADER_FONT);
        DefaultTableCellRenderer renderer = (DefaultTableCellRenderer) table.getTableHeader().getDefaultRenderer();
        renderer.setHorizontalAlignment(SwingConstants.CENTER);
        table.setAutoCreateRowSorter(true);

        table.setRowSelectionAllowed(true);
        table.setSelectionMode(ListSelectionModel.SINGLE_SELECTION);
        table.setColumnSelectionAllowed(false);
        table.getSelectionModel().setSelectionInterval(0, 0);

        JScrollPane jScroll = new JScrollPane(table);
        jScroll.createVerticalScrollBar();
        jScroll.setVerticalScrollBarPolicy(ScrollPaneConstants.VERTICAL_SCROLLBAR_AS_NEEDED);
        table.setPreferredScrollableViewportSize(new Dimension(TABLE_WIDTH - 20, TABLE_HEIGHT ));

        int row = 0;
        gbc.gridx = row;
        gbc.gridy = 0; // столбец
        gbc.gridwidth = 3; // сколько столбцов занимает элемент

        // "лишнее" пространство оставлять пустым
        gbc.weightx = 0;
        gbc.weighty = 0;

        gbc.fill = GridBagConstraints.HORIZONTAL; // заполнять по горизонтали
        gbc.anchor = GridBagConstraints.BELOW_BASELINE; // привязка к центру

        gbc.insets = new Insets(10, 5, 10, 5);// отступы
        gbc.ipadx = 5;
        gbc.ipady = 5;

        add(jScroll, gbc);

        gbc.gridy = ++row;
        gbc.gridwidth = 1;
        gbc.gridx = GridBagConstraints.RELATIVE;
        gbc.insets = new Insets(10, 5, 10, 0);
        add(addBtn, gbc);
        gbc.insets = new Insets(10, 5, 10, 0);
        add(editBtn, gbc);
        gbc.insets = new Insets(10, 5, 10, 5);
        add(deleteBtn, gbc);

        setMinimumSize(new Dimension(TABLE_WIDTH, TABLE_HEIGHT));
        setAlignmentX(CENTER_ALIGNMENT);
    }

    public Button getAddBtn() {
        return addBtn;
    }

    public Button getEditBtn() {
        return editBtn;
    }

    public Button getDeleteBtn() {
        return deleteBtn;
    }

    public INamed getSelectedItem() {
        if (values.size() == 0) return null;
        else return values.get(table.convertRowIndexToModel(table.getSelectedRow()));
    }
}
